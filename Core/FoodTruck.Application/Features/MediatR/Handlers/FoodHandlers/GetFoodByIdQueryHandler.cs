using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class GetFoodByIdQueryHandler : IRequestHandler<GetFoodByIdQuery, GetFoodByIdQueryResult>
    {
        private readonly IFoodRepository _repository;

        public GetFoodByIdQueryHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetFoodByIdQueryResult> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
        {
            var food = await _repository.GetFoodByIdAsync(request.Id, cancellationToken);

            return new GetFoodByIdQueryResult
            {
                CountryName = food.Country.Name,
                Description = food.Food.Description,
                ImageLocalPath = food.Food.ImageLocalPath,
                CategoryName = food.Food.Category.CategoryName,
                ImageUrl = food.Food.ImageUrl,
                FoodName = food.Food.Name,
                FoodId = food.Food.FoodId,
                MoodNames = food.Moods.Select(x => x.Name).ToList(),
                TasteNames = food.Tastes.Select(x => x.Name).ToList(),
                IngredientNames = food.Ingredients.Select(x => x.Name).ToList(),
                Price = food.Food.price,
                CategoryId = food.Food.CategoryId,
                CountryId = food.Food.CountryId
            };
        }
    }
}
