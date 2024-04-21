using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CartQueries
{
    public class GetCartCouponApplyQuery : IRequest<GetCartCouponApplyQueryResult>
    {
        public GetCartCouponApplyQuery(string userId, string couponCode)
        {
            UserId = userId;
            this.couponCode = couponCode;
        }
        public string UserId { get; set; }
        public string couponCode { get; set; }
    }
}
