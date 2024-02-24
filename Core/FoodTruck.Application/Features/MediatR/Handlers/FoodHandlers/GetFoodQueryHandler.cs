using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, List<GetFoodQueryResult>>
    {
        private readonly IFoodRepository _repository;

        public GetFoodQueryHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodQueryResult>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Food> foods = await _repository.GetFoodsWithAll();
            return foods.Select(x => new GetFoodQueryResult
            {
                CountryName = x.Country.Name,
                Description = x.Description,
                ImageLocalPath = x.ImageLocalPath,
                CategoryName = x.Category.CategoryName,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price,
                FoodId = x.FoodId,
                MoodName = x.FoodMoods.FirstOrDefault(z => z.FoodId == x.FoodId).Mood.Name,
                TasteName = x.FoodTastes.FirstOrDefault(y => y.FoodId == x.FoodId).Taste.Name
            }).ToList();
        }
    }
}
