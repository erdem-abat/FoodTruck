using FoodTruck.Application.Features.MediatR.Results.IngredientResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.IngredientQueries
{
    public class GetIngredientQuery : IRequest<List<GetIngredientQueryResult>>
    {
    }
}
