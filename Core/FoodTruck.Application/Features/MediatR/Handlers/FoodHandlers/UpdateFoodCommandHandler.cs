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
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand>
    {
        private readonly IRepository<Food> _repository;

        public UpdateFoodCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.FoodId);
            value.CountryId = request.CountryId;
            value.CategoryId = request.CategoryId;
            value.ImageLocalPath = request.ImageLocalPath;
            value.Price = request.Price;
            value.Description= request.Description;
            value.ImageUrl= request.ImageUrl;
            value.Name= request.Name;            
            await _repository.UpdateAsync(value);
        }
    }
}
