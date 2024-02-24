using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class GetFoodMainPageQueryHandler : IRequestHandler<GetFoodMainPageQuery, List<GetFoodMainPageQueryResult>>
    {
        private readonly IFoodRepository _repository;

        public GetFoodMainPageQueryHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodMainPageQueryResult>> Handle(GetFoodMainPageQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Food> foods = await _repository.GetFoodsWithCategory();
            return foods.Select(x => new GetFoodMainPageQueryResult
            {
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }
    }
}
