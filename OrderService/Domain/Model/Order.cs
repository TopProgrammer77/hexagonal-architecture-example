using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Model;

[Table("orders")]
public class Order : AbstractEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_id", TypeName = "bigint")]
    public long CustomerId { get; set; }

    [Column("total", TypeName = "decimal(10, 2)")]
    public double Total { get; set; }

    [Column("status", TypeName = "smallint")]
    public OrderStatus? Status { get; set; }

    [Column("creation_date", TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    //@OneToMany(fetch = FetchType.LAZY, mappedBy = "order", cascade = CascadeType.ALL, orphanRemoval = true)
    //[JoinColumn("order_id")]
    public ICollection<OrderItem>? OrderItems { get; set; }

    public Order(long customerId, double total)
    {
        CustomerId = customerId;
        Total = total;
        CreationDate = DateTime.UtcNow;
    }

    // required by Entity Framework
    protected Order()
    {
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        Order order = (Order)obj;
        return Id == order.Id &&
                CustomerId == order.CustomerId &&
                Total == order.Total &&
                CreationDate == order.CreationDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CustomerId, Total, CreationDate);
    }
}