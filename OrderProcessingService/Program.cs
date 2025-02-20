using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using Microsoft.EntityFrameworkCore;
using RyanCorp.OrderProcessingService.Data;
using RyanCorp.OrderProcessingService.Services.Interfaces;
using RyanCorp.OrderProcessingService.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var azureQueueStorageSection = configuration.GetSection("AzureQueueStorage");

builder.Services.AddDbContext<OrderProcessingDbContext>(options =>
    options.UseInMemoryDatabase("OrderProcessingDB"));

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.UseCredential(new DefaultAzureCredential());

    clientBuilder.AddQueueServiceClient(azureQueueStorageSection)
        .ConfigureOptions(options => options.MessageEncoding = QueueMessageEncoding.Base64);
});

builder.Services.AddSingleton(provider =>
{
    var queueServiceClient = provider.GetRequiredService<QueueServiceClient>();
    return queueServiceClient.GetQueueClient("order-queue");
});

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
