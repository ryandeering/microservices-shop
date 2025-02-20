using Microsoft.EntityFrameworkCore;
using RyanCorp.OrderProcessingService.Models;

namespace RyanCorp.OrderProcessingService.Data;

public class OrderProcessingDbContext : DbContext
{
    public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
}
