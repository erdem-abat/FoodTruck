using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.TruckCommands
{
    public class CreateTruckCommand : IRequest<GetTruckCreateCommandResult>
    {
        public string TruckName { get; set; }
        public List<Food> Foods { get; set; }
        public List<Chef> Chefs { get; set; }
    }
}
