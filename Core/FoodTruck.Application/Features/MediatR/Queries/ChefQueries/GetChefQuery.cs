using FoodTruck.Application.Features.MediatR.Results.ChefResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.ChefQueries
{
    public class GetChefQuery : IRequest<List<GetChefQueryResult>>
    {
    }
}