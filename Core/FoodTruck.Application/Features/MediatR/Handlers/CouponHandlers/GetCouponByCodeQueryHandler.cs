using FoodTruck.Application.Features.MediatR.Queries.CouponQueries;
using FoodTruck.Application.Features.MediatR.Results.CouponResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CouponHandlers
{
    public class GetCouponByCodeQueryHandler : IRequestHandler<GetCouponByCodeQuery, GetCouponByCodeQueryResult>
    {
        private readonly IRepository<Coupon> _repository;

        public GetCouponByCodeQueryHandler(IRepository<Coupon> repository)
        {
            _repository = repository;
        }

        public async Task<GetCouponByCodeQueryResult> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByFilterAsync(x => x.CouponCode.ToLower() == request.Code.ToLower());
            return new GetCouponByCodeQueryResult
            {
                CouponCode = value.CouponCode,
                CouponId =value.CouponId,
                DiscountAmount = value.DiscountAmount,
                minAmount = value.minAmount
            };
        }
    }
}
