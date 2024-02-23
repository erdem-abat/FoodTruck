using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class GetFoodQueryHandler : IRequestHandler<GetFoodQuery, List<GetFoodQueryResult>>
    {
        private readonly IRepository<Food> _repository;

        public GetFoodQueryHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodQueryResult>> Handle(GetFoodQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Food> foods = await _repository.GetAllAsync();
            return foods.Select(x => new GetFoodQueryResult
            {
                Country = x.Country,
                Description = x.Description,
                ImageLocalPath = x.ImageLocalPath,
                CategoryName = "deneme",
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price,
                FoodId = x.FoodId
            }).ToList();
        }
    }
}
