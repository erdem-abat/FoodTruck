using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.FoodHandlers
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand>
    {
        private readonly IRepository<Food> _repository;

        public CreateFoodCommandHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Food
            {
                CategoryId = request.CategoryId,
                Country = request.Country,
                Description = request.Description,
                ImageLocalPath = request.ImageLocalPath,
                ImageUrl = request.ImageUrl,
                Name = request.Name,
                Price = request.Price
            });
        }
    }
}
