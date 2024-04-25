using FoodTruck.Dto.RestaurantDtos;

namespace FoodTruck.Application.Features.MediatR.Results.RestaurantResults
{
    public class CreateRestaurantCommandResult
    {
        public RestaurantDto RestaurantDto { get; set; }
    }
}