using FoodTruck.Application.Features.MediatR.Commands.CouponCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _couponRepository.CreateCoupon(new Coupon
            {
                CouponCode = request.CouponCode,
                DiscountAmount = request.DiscountAmount,
                minAmount = request.minAmount
            });
        }
    }
}
