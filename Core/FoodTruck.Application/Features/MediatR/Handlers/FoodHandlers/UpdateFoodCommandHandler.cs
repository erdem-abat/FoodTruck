using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

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
            value.Description= request.Description;
            value.ImageUrl= request.ImageUrl;
            value.Name= request.Name;  
            value.price = request.Price;
            
            await _repository.UpdateAsync(value);
        }
    }
}
