using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodRestaurant
{
    public int FoodRestaurantId { get; set; }
    public int FoodId { get; set; }
    [NotMapped]
    public Food Food { get; set; }
    public int RestaurantId { get; set; }
    [NotMapped]
    public Restaurant Restaurant { get; set; }
    public double Price { get; set; }
}
