using FoodTruck.Application.Features.MediatR.Results.SeatResults;
using FoodTruck.Dto.SeatDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.SeatCommands
{
    public class CreateSeatPlanCommand : IRequest<CreateSeatPlanCommandResult>
    {
        public SeatPlanDto SeatPlanDto { get; set; }
    }
}