using FoodTruck.Domain.Entities;
using FoodTruck.Dto.TableDtos;

namespace FoodTruck.Dto.RestaurantDtos
{
    public class RestaurantHeaderDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public bool IsAlcohol { get; set; }
        public IEnumerable<RestaurantDetailDto> RestaurantDetailDto { get; set; }
    }
}