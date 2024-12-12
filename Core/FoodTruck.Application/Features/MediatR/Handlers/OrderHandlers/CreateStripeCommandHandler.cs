using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.OrderDtos;
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
            var value = await _repository.CreateStripeAsync(new StripeRequestDto
            {
                ApprovedUrl = request.ApprovedUrl,
                CancelUrl = request.CancelUrl,
                orderHeaderDto = request.orderHeaderDto,
                StripeSessionId = request.StripeSessionId,
                StripeSessionUrl = request.StripeSessionUrl
            });

            return new CreateStripeCommandResult
            {
                stripeRequestDto = value
            };
        }
    }
}
