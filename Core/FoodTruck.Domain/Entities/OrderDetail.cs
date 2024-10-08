using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    [NotMapped]
    public Order Order { get; set; }
    public int Count { get; set; }
    public int FoodId { get; set; }
    [NotMapped]
    public Food Food { get; set; }
    public double Price { get; set; }
}