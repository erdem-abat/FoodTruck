using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.OrderHandlers
{
    public class CreateStripeCommandHandler : IRequestHandler<CreateStripeCommand, CreateStripeCommandResult>
    {
        private readonly IOrderRepository _repository;

        public CreateStripeCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateStripeCommandResult> Handle(CreateStripeCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.CreateStripeAsync(request.stripeRequestDto);

            return new CreateStripeCommandResult
            {
                stripeRequestDto = value
            };
        }
    }
}
