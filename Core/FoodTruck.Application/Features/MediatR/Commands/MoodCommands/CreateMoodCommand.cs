using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Application.Features.MediatR.Commands.MoodCommands;

public class CreateMoodCommand : IRequest<bool>
{
    public string Name { get; set; }
}