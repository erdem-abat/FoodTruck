using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.FoodDtos;
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
            IEnumerable<FoodWithAllDto> foods = await _repository.GetFoodsWithAll(cancellationToken);
           
            return foods.Select(x => new GetFoodQueryResult
            {
                CountryName = x.Country.Name,
                Description = x.Food.Description,
                ImageLocalPath = x.Food.ImageLocalPath,
                CategoryName = x.Food.Category.CategoryName,
                ImageUrl = x.Food.ImageUrl,
                Name = x.Food.Name,
                Price = x.Food.Price,
                FoodId = x.Food.FoodId,
                MoodName = x.Food.FoodMoods.FirstOrDefault(z => z.FoodId == x.Food.FoodId)?.Mood?.Name,
                TasteName = x.Food.FoodTastes.FirstOrDefault(y => y.FoodId == x.Food.FoodId)?.Taste?.Name
            }).ToList();
        }
    }
}