using FoodTruck.Application.Features.MediatR.Queries.CartQueries;
using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class GetCartCouponApplyQueryHandler : IRequestHandler<GetCartCouponApplyQuery, GetCartCouponApplyQueryResult>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartCouponApplyQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetCartCouponApplyQueryResult> Handle(GetCartCouponApplyQuery request, CancellationToken cancellationToken)
        {
            var value = await _cartRepository.ApplyCouponAsync(request.UserId, request.couponCode);

            return new GetCartCouponApplyQueryResult
            {
                IsApply = value
            };
        }
    }
}
