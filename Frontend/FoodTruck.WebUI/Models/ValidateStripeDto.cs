namespace FoodTruck.WebUI.Models
{
    public class ValidateStripeDto
    {
        public int orderId { get; set; }
        public int? truckId { get; set; }
    }
}
