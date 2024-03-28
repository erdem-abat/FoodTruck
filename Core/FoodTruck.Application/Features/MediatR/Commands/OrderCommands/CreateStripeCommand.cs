using FoodTruck.Dto.OrderDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class CreateStripeCommand : IRequest
    {
        public StripeRequestDto stripeRequestDto { get; set; }
    }
}