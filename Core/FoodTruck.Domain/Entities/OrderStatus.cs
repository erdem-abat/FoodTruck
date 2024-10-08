namespace FoodTruck.Domain.Entities;

public class OrderStatus
{
    public int OrderStatusId { get; set; }
    public string StatusName { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}
