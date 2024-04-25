using FoodTruck.Application.Features.MediatR.Commands.SeatCommands;
using FoodTruck.Application.Features.MediatR.Results.SeatResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.SeatHandlers
{
    public class CreateSeatPlanCommandHandler : IRequestHandler<CreateSeatPlanCommand, CreateSeatPlanCommandResult>
    {
        private readonly IRestaurant _repository;

        public CreateSeatPlanCommandHandler(IRestaurant repository)
        {
            _repository = repository;
        }
        public async Task<CreateSeatPlanCommandResult> Handle(CreateSeatPlanCommand request, CancellationToken cancellationToken)
        {
            return new CreateSeatPlanCommandResult
            {
                SeatPlanDto = await _repository.CreateSeatPlan(request.SeatPlanDto)
            };
        }
    }
}
