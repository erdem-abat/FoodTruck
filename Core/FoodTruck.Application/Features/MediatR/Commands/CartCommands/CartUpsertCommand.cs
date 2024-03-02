using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.CartCommands
{
    public class CartUpsertCommand : IRequest
    {
        public CartsDto CartsDto { get; set; }
    }
}
