using FoodTruck.Application.Features.MediatR.Queries.CouponQueries;
using FoodTruck.Application.Features.MediatR.Results.CouponResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CouponHandlers
{
    public class GetCouponQueryHandler : IRequestHandler<GetCouponQuery, List<GetCouponQueryResult>>
    {
        private readonly IRepository<Coupon> _repository;

        public GetCouponQueryHandler(IRepository<Coupon> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCouponQueryResult>> Handle(GetCouponQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Coupon> values = await _repository.GetAllAsync();
            return values.Select(x => new GetCouponQueryResult
            {
                CouponCode = x.CouponCode,
                CouponId = x.CouponId,
                DiscountAmount = x.DiscountAmount,
                minAmount = x.minAmount
            }).ToList();
        }
    }
}
