using FoodTruck.Application.Features.MediatR.Queries.ResturantQueries;
using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.RestaurantHandlers;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, GetRestaurantByIdQueryResult>
{
    private readonly IRestaurant _repository;

    public GetRestaurantByIdQueryHandler(IRestaurant repository)
    {
        _repository = repository;
    }

    public async Task<GetRestaurantByIdQueryResult> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await _repository.GetRestaurantById(request.restaurantId);
        return new GetRestaurantByIdQueryResult
        {
            RestaurantInfoDto = value
        };
    }
}