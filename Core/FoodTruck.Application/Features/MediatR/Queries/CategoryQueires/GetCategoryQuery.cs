using FoodTruck.Application.Features.MediatR.Results.CategoryResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CategoryQueires
{
    public class GetCategoryQuery : IRequest<List<GetCategoryQueryResult>>
    {
    }
}
