using FoodTruck.Dto.CouponDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Application.Features.MediatR.Commands.CouponCommands
{
    public class CouponCreateCommand : IRequest
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int minAmount { get; set; }
    }
}
