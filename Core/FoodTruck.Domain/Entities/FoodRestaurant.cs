namespace FoodTruck.Domain.Entities;

public class FoodRestaurant
{
    public int FoodRestaurantId { get; set; }
    public int FoodId { get; set; }
    public Food Food { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public double Price { get; set; }
}
