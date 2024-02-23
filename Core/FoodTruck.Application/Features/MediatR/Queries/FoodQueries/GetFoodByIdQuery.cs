using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Queries.FoodQueries
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