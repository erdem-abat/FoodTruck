namespace FoodTruck.WebUI.Models
{
    public class OrderDetailsDto
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}