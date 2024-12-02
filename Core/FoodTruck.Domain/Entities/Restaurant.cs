namespace FoodTruck.Domain.Entities;

public class Restaurant
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public virtual IEnumerable<Table> Tables { get; set; }
    public virtual IEnumerable<RestaurantDetail> RestaurantDetails { get; set; }
    public virtual IEnumerable<FoodRestaurant> FoodRestaurants { get; set; }
    public virtual IEnumerable<Chef> Chefs { get; set; }
    public virtual IEnumerable<Advertise> Advertises { get; set; }
    public bool IsAlcohol { get; set; }
    public bool IsApproved { get; set; } = false;
    public virtual IEnumerable<RestaurantUser> RestaurantUsers { get; set; }
}