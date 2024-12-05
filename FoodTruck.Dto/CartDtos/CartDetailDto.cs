using System.Text.Json.Serialization;
using FoodTruck.Dto.FoodDtos;

namespace FoodTruck.Dto.CartDtos
{
    public class CartDetailDto
    {
        public int CartDetailId { get; set; }
        public int Count { get; set; }

        public int CartHeaderId { get; set; }
        [JsonIgnore]
        public CartHeaderDto CartHeader { get; set; }
        public int FoodId { get; set; }
        [JsonIgnore]
        public FoodDto? Food { get; set; }
    }
}
