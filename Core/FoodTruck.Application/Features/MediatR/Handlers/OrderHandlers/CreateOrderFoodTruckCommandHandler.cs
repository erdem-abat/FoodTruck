using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.OrderHandlers
{
    public class CreateOrderFoodTruckCommandHandler : IRequestHandler<CreateOrderFoodTruckCommand, CreateOrderFoodTruckCommandResult>
    {
        private readonly IOrderRepository _repository;

        public CreateOrderFoodTruckCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateOrderFoodTruckCommandResult> Handle(CreateOrderFoodTruckCommand request, CancellationToken cancellationToken)
        {
            var value = _repository.CreateOrderFoodTruckAsync(request.FoodTruckCartsDto);
            return new CreateOrderFoodTruckCommandResult
            {
                orderHeaderDto = value.Result
            };
        }
    }
}
