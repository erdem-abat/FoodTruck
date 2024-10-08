using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodTruck
{
    public int FoodTruckId { get; set; }
    public int FoodId { get; set; }
    [NotMapped]
    public Food Food { get; set; }
    public int TruckId { get; set; }
    [NotMapped]
    public Truck Truck { get; set; }
    public int Stock { get; set; }
}
