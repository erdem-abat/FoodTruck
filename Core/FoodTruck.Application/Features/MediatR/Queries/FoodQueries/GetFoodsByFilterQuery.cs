using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using FoodTruck.Dto.FoodDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Queries.FoodQueries
{
    public class GetFoodsByFilterQuery : IRequest<List<GetFoodsByFilterQueryResult>>
    {
        public GetFoodsByFilterParameters getFoodsByFilterParameters { get; set; }

        public GetFoodsByFilterQuery(GetFoodsByFilterParameters getFoodsByFilterParameters)
        {
            this.getFoodsByFilterParameters = getFoodsByFilterParameters;
        }
    }
}