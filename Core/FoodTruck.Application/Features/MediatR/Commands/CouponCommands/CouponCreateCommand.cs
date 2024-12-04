using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.CouponCommands
{
    public class CouponCreateCommand : IRequest
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int minAmount { get; set; }
    }
}
