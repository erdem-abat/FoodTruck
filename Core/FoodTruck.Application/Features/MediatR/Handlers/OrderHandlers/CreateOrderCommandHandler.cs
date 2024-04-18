using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.OrderHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var value = _repository.CreateOrder(request.CartsDto);
            return new CreateOrderCommandResult
            {
                orderHeaderDto = value.Result
            };
        }
    }
}
