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
        public async Task Handle(CartUpsertCommand request, CancellationToken cancellationToken)
        {
            await _repository.CartUpsertAsync(request.CartsDto);
        }
    }
}