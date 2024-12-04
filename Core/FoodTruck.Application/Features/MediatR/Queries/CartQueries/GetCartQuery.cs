using FoodTruck.Application.Features.MediatR.Results.CartResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CartQueries
{
    public class GetCartQuery : IRequest<GetCartQueryResult>
    {
        public string UserId { get; set; }

        public GetCartQuery(string userId)
        {
            UserId = userId;
        }
    }
}
