using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Dto.OrderDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class CreateStripeCommand : IRequest<CreateStripeCommandResult>
    {
        public StripeRequestDto stripeRequestDto { get; set; }
    }
}