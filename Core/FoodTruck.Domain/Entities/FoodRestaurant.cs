using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodRestaurant
{
    public int FoodId { get; set; }
    [NotMapped]
    public virtual Food Food { get; set; }
    public int RestaurantId { get; set; }
    [NotMapped]
    public virtual Restaurant Restaurant { get; set; }
    public double Price { get; set; }
}
