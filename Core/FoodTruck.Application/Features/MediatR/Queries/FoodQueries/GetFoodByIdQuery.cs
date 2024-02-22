using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.FoodQueries
{
    public class GetFoodByIdQuery : IRequest<GetFoodByIdQueryResult>
    {
        public int Id { get; set; }

        public GetFoodByIdQuery(int id)
        {
            Id = id;
        }
    }
}