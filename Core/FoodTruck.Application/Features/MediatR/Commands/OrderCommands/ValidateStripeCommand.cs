using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class ValidateStripeCommand : IRequest<ValidateStripeCommandResult>
    {
        public int orderId { get; set; }
    }
}
