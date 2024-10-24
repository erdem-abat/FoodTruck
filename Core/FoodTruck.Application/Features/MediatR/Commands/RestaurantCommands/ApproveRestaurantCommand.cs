using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;

public class ApproveRestaurantCommand : IRequest<bool>
{
    public int RestaurantId { get; set; }
    public bool IsApproved { get; set; }
}