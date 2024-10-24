using FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.RestaurantHandlers
{
    public class ApproveRestaurantCommandHandler : IRequestHandler<ApproveRestaurantCommand, bool>
    {
        private readonly IRestaurant _restaurant;

        public ApproveRestaurantCommandHandler(IRestaurant restaurant)
        {
            _restaurant = restaurant;
        }

        public async Task<bool> Handle(ApproveRestaurantCommand request, CancellationToken cancellationToken)
        {
            return await _restaurant.ApproveRestaurant(request.IsApproved, request.RestaurantId);
        }
    }
}
