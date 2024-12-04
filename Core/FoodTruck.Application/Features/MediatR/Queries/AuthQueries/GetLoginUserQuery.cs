using FoodTruck.Application.Features.MediatR.Results.AuthResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.AuthQueries
{
    public class GetLoginUserQuery : IRequest<GetLoginUserQueryResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
