namespace FoodTruck.WebUI.Models
{
    public class CartDetailsDto
    {
        public int cartDetailId { get; set; }
        public int cartHeaderId { get; set; }
        public CartHeaderDto? cartHeader { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
        public int count { get; set; }
    }
}
