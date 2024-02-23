using MediatR;

namespace FoodTruck.Application.Features.MediateR.Commands.FoodCommands
{
    public class RemoveFoodCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveFoodCommand(int id)
        {
            Id = id;
        }
    }
}