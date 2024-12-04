using FoodTruck.Application.Features.MediatR.Commands.TruckCommands;
using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.TruckHandlers
{
    public class CreateTruckCommandQueryHandler : IRequestHandler<CreateTruckCommand, GetTruckCreateCommandResult>
    {
        private readonly ITruckRepository _repository;

        public CreateTruckCommandQueryHandler(ITruckRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTruckCreateCommandResult> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.CreateTruckAsync(new Truck
            {
                TruckName = request.TruckName
            }, request.foodIdsWithStocks, request.ChefIds);

            return new GetTruckCreateCommandResult
            {
                IsCreated = value
            };
        }
    }
}
