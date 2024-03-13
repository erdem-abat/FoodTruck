using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.CouponCommands
{
    public class CouponUpdateCommand : IRequest
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int minAmount { get; set; }
    }
}
