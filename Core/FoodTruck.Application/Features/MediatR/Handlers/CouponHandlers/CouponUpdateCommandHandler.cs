using FoodTruck.Application.Features.MediatR.Commands.CouponCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CouponHandlers
{
    public class CouponUpdateCommandHandler : IRequestHandler<CouponUpdateCommand>
    {
        private IRepository<Coupon> _repository;

        public CouponUpdateCommandHandler(IRepository<Coupon> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CouponUpdateCommand request, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(new Coupon
            {
                CouponCode = request.CouponCode,
                CouponId = request.CouponId,
                DiscountAmount = request.DiscountAmount,
                minAmount = request.minAmount
            });
        }
    }
}
