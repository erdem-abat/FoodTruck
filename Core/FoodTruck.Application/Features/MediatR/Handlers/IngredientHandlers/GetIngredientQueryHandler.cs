using FoodTruck.Application.Features.MediatR.Queries.IngredientQueries;
using FoodTruck.Application.Features.MediatR.Results.IngredientResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.IngredientHandlers
{
    public class GetIngredientQueryHandler : IRequestHandler<GetIngredientQuery, List<GetIngredientQueryResult>>
    {
        private readonly IRepository<Ingredient> _repository;

        public GetIngredientQueryHandler(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetIngredientQueryResult>> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new GetIngredientQueryResult
            {
                IngredientId = x.IngredientId,
                Name = x.Name,
                bigImageUrl = x.bigImageUrl,
                price = x.price,
                smallImageUrl = x.smallImageUrl
            }).ToList();
        }
    }
}