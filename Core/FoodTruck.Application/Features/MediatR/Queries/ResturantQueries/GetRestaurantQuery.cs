using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.ResturantQueries;

public class GetRestaurantQuery : IRequest<List<GetRestaurantQueryResult>>
{
    
}