using System.Text.Json;
using Azure.Storage.Queues;
using RyanCorp.OrderProcessingService.Data;
using RyanCorp.OrderProcessingService.Models;
using RyanCorp.OrderProcessingService.Services.Interfaces;

namespace RyanCorp.OrderProcessingService.Services;

public class OrderService : IOrderService
{
    private readonly OrderProcessingDbContext _dbContext;
    private readonly QueueClient _queueClient;

    public OrderService(OrderProcessingDbContext dbContext, QueueClient queueClient)
    {
        _dbContext = dbContext;
        _queueClient = queueClient;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.Id = Guid.NewGuid();
        order.OrderDate = DateTime.UtcNow;
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        var orderPlacedEvent = new OrderPlaced 
        { 
            OrderId = order.Id, 
            Timestamp = DateTime.UtcNow 
        };

        var message = JsonSerializer.Serialize(orderPlacedEvent);
        await _queueClient.SendMessageAsync(message);

        return order;
    }
}
