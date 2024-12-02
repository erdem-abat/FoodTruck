namespace FoodTruck.Domain.Entities;

public class OrderStatus
{
    public int OrderStatusId { get; set; }
    public string StatusName { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
}
