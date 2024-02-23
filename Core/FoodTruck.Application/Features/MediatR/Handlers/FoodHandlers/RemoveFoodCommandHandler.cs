using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class RemoveFoodCommandHandler : IRequestHandler<RemoveFoodCommand>
    {
        private readonly IRepository<Food> _repository;

        public RemoveFoodCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveFoodCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
}
