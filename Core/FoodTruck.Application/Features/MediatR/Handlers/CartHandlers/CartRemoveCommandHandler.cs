using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class CartRemoveCommandHandler : IRequestHandler<CartRemoveCommand, GetCartRemoveCommandResult>
    {
        private readonly ICartRepository _cartRepository;

        public CartRemoveCommandHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetCartRemoveCommandResult> Handle(CartRemoveCommand request, CancellationToken cancellationToken)
        {
            return new GetCartRemoveCommandResult
            {
                IsDeleted = _cartRepository.CartRemove(request.cartDetailId).Result
            };
        }
    }
}