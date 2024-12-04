using FoodTruck.Application.Features.MediatR.Results.CouponResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CouponQueries
{
    public class GetCouponByCodeQuery : IRequest<GetCouponByCodeQueryResult>
    {
        public string Code { get; set; }

        public GetCouponByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
