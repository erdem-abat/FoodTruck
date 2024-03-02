using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class CartUpsertCommandHandler : IRequestHandler<CartUpsertCommand>
    {
        private readonly ICartRepository _repository;

        public CartUpsertCommandHandler(ICartRepository repository)
        {
            _repository = repository;
        }
        public Task Handle(CartUpsertCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}