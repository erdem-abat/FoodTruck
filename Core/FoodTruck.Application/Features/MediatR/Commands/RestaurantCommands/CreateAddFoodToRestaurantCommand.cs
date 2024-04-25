using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands
{
    public class CreateAddFoodToRestaurantCommand : IRequest<CreateAddFoodToRestaurantCommandResult>
    {
        public int FoodId { get; set; }
        public int RestaurantId { get; set; }
        public double Price { get; set; }
    }
}
