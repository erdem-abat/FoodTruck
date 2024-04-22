using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.CartCommands
{
    public class FoodTruckCartUpsertCommand : IRequest
    {
        public FoodTruckCartsDto FoodTruckCartsDto { get; set; }
    }
}
