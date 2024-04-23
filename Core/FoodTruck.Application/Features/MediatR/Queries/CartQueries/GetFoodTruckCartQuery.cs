using FoodTruck.Application.Features.MediatR.Results.CartResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CartQueries
{
    public class GetFoodTruckCartQuery : IRequest<GetFoodTruckCartQueryResult>
    {
        public string UserId { get; set; }

        public GetFoodTruckCartQuery(string userId)
        {
            UserId = userId;
        }
    }
}
