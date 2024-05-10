using FoodTruck.Application.Features.MediatR.Queries.ResturantQueries;
using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.RestaurantHandlers;

public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, List<GetRestaurantQueryResult>>
{
    private readonly IRestaurant _repository;

    public GetRestaurantQueryHandler(IRestaurant repository)
    {
        _repository = repository;
    }
    public async Task<List<GetRestaurantQueryResult>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        var values = await _repository.GetRestaurants();
        
        return values.Select(x => new GetRestaurantQueryResult
        {
            RestaurantInfoDto = x
        }).ToList();
    }
}