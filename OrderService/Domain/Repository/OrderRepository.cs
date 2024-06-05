using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Model;

namespace OrderService.Domain.Repository;

public class OrderRepository : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public OrderRepository(DbContextOptions<OrderRepository> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
    }
}
