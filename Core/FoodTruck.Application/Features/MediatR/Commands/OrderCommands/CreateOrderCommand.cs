using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest
    {
        public CartsDto CartsDto { get; set; }
    }
}