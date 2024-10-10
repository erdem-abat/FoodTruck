using FoodTruck.Application.Features.MediatR.Results.IngredientResults;
using FoodTruck.Application.Features.MediatR.Results.MoodResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.IngredientQueries
{
    public class GetIngredientQuery : IRequest<List<GetIngredientQueryResult>>
    {
    }
}
