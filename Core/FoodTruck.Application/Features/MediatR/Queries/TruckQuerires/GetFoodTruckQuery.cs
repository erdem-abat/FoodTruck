using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.TruckQuerires
{
    public class GetFoodTruckQuery : IRequest<List<GetFoodTruckQueryResult>>
    {
    }
}
