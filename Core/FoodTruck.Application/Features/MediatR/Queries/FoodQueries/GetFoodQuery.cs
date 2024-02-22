using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.FoodQueries
{
    public class GetFoodQuery : IRequest<List<GetFoodQueryResult>>
    {
    }
}