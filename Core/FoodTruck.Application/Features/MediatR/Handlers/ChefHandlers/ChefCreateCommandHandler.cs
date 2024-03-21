using FoodTruck.Application.Features.MediatR.Commands.ChefCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.ChefHandlers
{
    public class ChefCreateCommandHandler : IRequestHandler<ChefCreateCommand>
    {
        private readonly IRepository<Chef> _repository;

        public ChefCreateCommandHandler(IRepository<Chef> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ChefCreateCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Chef
            {
                Name = request.Name,
                Popularity =request.Popularity,
                TruckId = request.TruckId == 0 ? 1 : request.TruckId
            });
        }
    }
}
