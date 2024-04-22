using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.TruckCommands
{
    public class CreateTruckCommand : IRequest<GetTruckCreateCommandResult>
    {
        public string TruckName { get; set; }
        public List<int[][]> foodIdsWithStocks { get; set; }
        public List<int> ChefIds { get; set; }
    }
}
