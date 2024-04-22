using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class FoodTruckCartUpsertCommandHandler : IRequestHandler<FoodTruckCartUpsertCommand>
    {
        private readonly ICartRepository _repository;

        public FoodTruckCartUpsertCommandHandler(ICartRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(FoodTruckCartUpsertCommand request, CancellationToken cancellationToken)
        {
            await _repository.FoodTruckCartUpsert(request.FoodTruckCartsDto);
        }
    }
}