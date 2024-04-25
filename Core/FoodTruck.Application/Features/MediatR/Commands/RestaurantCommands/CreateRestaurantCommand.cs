using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using FoodTruck.Dto.RestaurantDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands
{
    public class CreateRestaurantCommand : IRequest<CreateRestaurantCommandResult>
    {
        public RestaurantDto RestaurantDto { get; set; }
    }
}