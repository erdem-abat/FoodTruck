using FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;
using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.RestaurantHandlers
{
    public class CreateAddFoodToRestaurantCommandHandler : IRequestHandler<CreateAddFoodToRestaurantCommand, CreateAddFoodToRestaurantCommandResult>
    {
        private readonly IRestaurant _repository;

        public CreateAddFoodToRestaurantCommandHandler(IRestaurant repository)
        {
            _repository = repository;
        }
        public async Task<CreateAddFoodToRestaurantCommandResult> Handle(CreateAddFoodToRestaurantCommand request, CancellationToken cancellationToken)
        {
            return new CreateAddFoodToRestaurantCommandResult
            {
                Message = await _repository.AddFoodToRestaurantAsync(request.FoodId, request.RestaurantId, request.Price)
            };
        }
    }
}