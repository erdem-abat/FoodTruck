using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Queries.FoodQueries
{
    public class GetFoodQuery : IRequest<List<GetFoodQueryResult>>
    {
    }
}