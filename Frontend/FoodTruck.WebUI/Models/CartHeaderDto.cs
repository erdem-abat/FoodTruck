namespace FoodTruck.WebUI.Models
{
    public class CartHeaderDto
    {
        public int cartHeaderId { get; set; }
        public string? userId { get; set; }
        public string? couponCode { get; set; }
        public double discount { get; set; }
        public double cartTotal { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
    }
}
