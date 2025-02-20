namespace RyanCorp.OrderProcessingService.Models;

public class OrderPlaced
{
    public Guid OrderId { get; set; }
    public DateTime Timestamp { get; set; }
}

