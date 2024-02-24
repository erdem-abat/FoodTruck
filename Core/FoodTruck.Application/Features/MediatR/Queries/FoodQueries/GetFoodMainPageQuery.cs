using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Queries.FoodQueries
{
    public class GetFoodMainPageQuery : IRequest<List<GetFoodMainPageQueryResult>>
    {
    }
}