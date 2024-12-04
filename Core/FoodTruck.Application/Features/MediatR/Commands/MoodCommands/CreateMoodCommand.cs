using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.MoodCommands;

public class CreateMoodCommand : IRequest<bool>
{
    public string Name { get; set; }
}