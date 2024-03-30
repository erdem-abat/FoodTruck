using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.OrderHandlers
{
    public class ValidateStripeCommandHandler : IRequestHandler<ValidateStripeCommand, ValidateStripeCommandResult>
    {
        private readonly IOrderRepository _repository;

        public ValidateStripeCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValidateStripeCommandResult> Handle(ValidateStripeCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.ValidateStripe(request.orderId);

            return new ValidateStripeCommandResult
            {
                orderHeaderDto = value
            };
        }
    }
}
