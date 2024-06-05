using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Model;

public class OrderItem : AbstractEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("restaurant_id", TypeName = "bigint")]
    [MaxLength(5)]
    public long RestaurantId { get; set; }

    [Column("food_id", TypeName = "bigint")]
    [MaxLength(5)]
    public long FoodId { get; set; }

    [Column("price", TypeName = "bigint")]
    [MaxLength(5)]
    public long Price { get; set; }

    [Column("name", TypeName = "nvarchar")]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Column("description", TypeName = "nvarchar")]
    [MaxLength(200)]
    public string? Description { get; set; }

    //@JsonBackReference
    //@ManyToOne(cascade = CascadeType.ALL, fetch = FetchType.LAZY, targetEntity = Order.class, optional = false)
    //@JoinColumn(
    //        name = "order_id",
    //        foreignKey = @ForeignKey(name = "order_fk"),
    //        referencedColumnName = "id",
    //        nullable = false
    //)
    [ForeignKey("order_id")]
    public Order? Order { get; set; }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        var orderItem = (OrderItem)obj;
        return Id == orderItem.Id && RestaurantId == orderItem.RestaurantId && FoodId == orderItem.FoodId && Name == orderItem.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, RestaurantId, FoodId, Name);
    }
}