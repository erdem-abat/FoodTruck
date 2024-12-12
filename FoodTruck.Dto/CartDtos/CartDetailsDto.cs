using System.Text.Json.Serialization;
using FoodTruck.Dto.FoodDtos;

namespace FoodTruck.Dto.CartDtos
{
    public class CartDetailsDto
    {
        public int CartDetailId { get; set; }
        public int Count { get; set; }
        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
    }
}
