using FoodTruck.Application.Features.MediatR.Results.CouponResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CouponQueries
{
    public class GetCouponQuery : IRequest<List<GetCouponQueryResult>>
    {
    }
}
