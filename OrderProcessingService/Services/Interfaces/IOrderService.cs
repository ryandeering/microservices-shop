using RyanCorp.OrderProcessingService.Models;

namespace RyanCorp.OrderProcessingService.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
}
