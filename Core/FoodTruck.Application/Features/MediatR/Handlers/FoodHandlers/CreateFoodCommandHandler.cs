using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.FoodHandlers
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand>
    {
        private readonly IFoodRepository _repository;

        public CreateFoodCommandHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateFood(new Food
            {
                CategoryId = request.CategoryId,
                CountryId = request.CountryId,
                Description = request.Description,
                Image = request.Image,
                Name = request.Name,
                Price = request.Price
            }, request.FoodMoodIds, request.FoodTasteIds);
        }
    }
}