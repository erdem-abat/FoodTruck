namespace FoodTruck.Domain.Entities
{
    public class RestaurantUser
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}