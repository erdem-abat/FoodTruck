namespace FoodTruck.Dto.RestaurantDtos
{
    public class RestaurantDto
    {
        public RestaurantHeaderDto RestaurantHeaderDto { get; set; }
        public IEnumerable<RestaurantDetailDto> RestaurantDetailDto { get; set; }
        public string UserId { get; set; }
    }
}