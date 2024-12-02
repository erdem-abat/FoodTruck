using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodTruck
{
    public int FoodId { get; set; }
    [NotMapped]
    public virtual Food Food { get; set; }
    public int TruckId { get; set; }
    [NotMapped]
    public virtual Truck Truck { get; set; }
    public int Stock { get; set; }
}
