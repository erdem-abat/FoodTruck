using FoodTruck.Dto.RestaurantDtos;

namespace FoodTruck.Application.Features.MediatR.Results.RestaurantResults;

public class GetRestaurantQueryResult
{
    public RestaurantInfoDto RestaurantInfoDto { get; set; }
    public int totalTableCount { get; set; }
    public int availableTableCount { get; set; }
}