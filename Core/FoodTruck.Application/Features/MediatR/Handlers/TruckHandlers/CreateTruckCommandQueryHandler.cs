using Amazon.Runtime.Internal;
using FoodTruck.Application.Features.MediatR.Commands.TruckCommands;
using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.TruckHandlers
{
    public class CreateTruckCommandQueryHandler : IRequestHandler<CreateTruckCommand, GetTruckCreateCommandResult>
    {
        private readonly ITruckRepository _repository;

        public CreateTruckCommandQueryHandler(ITruckRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTruckCreateCommandResult> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.CreateTruck(new Truck
            {
                TruckName = request.TruckName
            }, request.foodIdsWithStocks, request.ChefIds);

            return new GetTruckCreateCommandResult
            {
                IsCreated = value
            };
        }
    }
}
