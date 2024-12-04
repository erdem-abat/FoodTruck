using System.Text.Json.Serialization;

namespace FoodTruck.Dto.RestaurantDtos
{
    public class RestaurantHeaderDto
    {
        [JsonIgnore]
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public bool IsAlcohol { get; set; }
        [JsonIgnore]
        public IEnumerable<RestaurantDetailDto> RestaurantDetailDto { get; set; }
    }
}