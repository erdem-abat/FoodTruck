using System.Text.Json.Serialization;
using FoodTruck.Dto.FoodDtos;

namespace FoodTruck.Dto.CartDtos
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int Count { get; set; }
        public int CartHeaderId { get; set; }
        [JsonIgnore]
        public CartHeaderDto? CartHeader { get; set; }
        public int FoodId { get; set; }        
        public FoodDto? Food { get; set; }
    }
}
