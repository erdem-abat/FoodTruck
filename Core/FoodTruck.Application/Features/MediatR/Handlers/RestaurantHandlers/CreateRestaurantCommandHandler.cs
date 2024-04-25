using FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;
using FoodTruck.Application.Features.MediatR.Results.RestaurantResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.RestaurantDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.RestaurantHandlers
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, CreateRestaurantCommandResult>
    {
        private readonly IRestaurant _repository;

        public CreateRestaurantCommandHandler(IRestaurant repository)
        {
            _repository = repository;
        }
        public async Task<CreateRestaurantCommandResult> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            return new CreateRestaurantCommandResult
            {
                RestaurantDto = await _repository.CreateRestaurant(request.RestaurantDto)
            };
        }
    }
}