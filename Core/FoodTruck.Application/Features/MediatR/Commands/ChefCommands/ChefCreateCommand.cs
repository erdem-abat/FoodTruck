using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.ChefCommands
{
    public class ChefCreateCommand : IRequest
    {
        public string Name { get; set; }
        public int Popularity { get; set; }
        public int TruckId { get; set; } = 1;
    }
}
