namespace RyanCorp.OrderProcessingService.Models;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
}
