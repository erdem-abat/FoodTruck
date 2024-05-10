using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.ResturantQueries;

public class GetRestaurantByIdQuery : IRequest<GetRestaurantByIdQueryResult>
{
    public GetRestaurantByIdQuery(int restaurantId)
    {
        this.restaurantId = restaurantId;
    }

    public int restaurantId { get; set; }
}