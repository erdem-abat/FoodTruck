using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<CreateOrderCommandResult>
    {
        public CartDto CartDto { get; set; }
    }
}