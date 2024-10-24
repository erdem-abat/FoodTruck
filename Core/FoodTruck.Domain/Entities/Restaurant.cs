namespace FoodTruck.Domain.Entities;

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public IEnumerable<Table> Tables { get; set; }
    public IEnumerable<RestaurantDetail> RestaurantDetails { get; set; }
    public IEnumerable<FoodRestaurant> FoodRestaurants { get; set; }
    public IEnumerable<Chef> Chefs { get; set; }
    public IEnumerable<Advertise> Advertises { get; set; }
    public bool IsAlcohol { get; set; }
    public bool IsApproved { get; set; } = false;
    public IEnumerable<RestaurantUser> RestaurantUsers { get; set; }
}