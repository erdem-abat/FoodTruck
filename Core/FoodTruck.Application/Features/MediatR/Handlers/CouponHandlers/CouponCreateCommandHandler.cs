using FoodTruck.Application.Features.MediatR.Commands.CouponCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CouponHandlers
{
    public class CouponCreateCommandHandler : IRequestHandler<CouponCreateCommand>
    {
        private ICouponRepository _couponRepository;

        public CouponCreateCommandHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task Handle(CouponCreateCommand request, CancellationToken cancellationToken)
        {
            await _couponRepository.CreateCouponAsync(new Coupon
            {
                CouponCode = request.CouponCode,
                DiscountAmount = request.DiscountAmount,
                minAmount = request.minAmount
            });
        }
    }
}
