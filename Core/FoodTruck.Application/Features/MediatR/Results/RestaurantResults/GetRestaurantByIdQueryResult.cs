using FoodTruck.Dto.RestaurantDtos;

namespace FoodTruck.Application.Features.MediatR.Results.RestaurantResults;

public class GetRestaurantByIdQueryResult
{
    public RestaurantInfoDto RestaurantInfoDto { get; set; }
}