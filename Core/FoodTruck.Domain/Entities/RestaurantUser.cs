namespace FoodTruck.Domain.Entities
{
    public class RestaurantUser
    {
        public string UserId { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}