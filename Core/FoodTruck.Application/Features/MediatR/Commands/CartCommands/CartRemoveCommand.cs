using FoodTruck.Application.Features.MediatR.Results.CartResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.CartCommands
{
    public class CartRemoveCommand : IRequest<GetCartRemoveCommandResult>
    {
        public int cartDetailId { get; set; }
    }
}
