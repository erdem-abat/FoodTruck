using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Features.MediatR.Queries.FoodQueries;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.FoodHandlers
{
    public class GetFoodsWithPagingQueryHandler : IRequestHandler<GetFoodsWithPagingQuery, List<GetFoodsWithPagingQueryResult>>
    {
        private readonly IFoodRepository _repository;

        public GetFoodsWithPagingQueryHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodsWithPagingQueryResult>> Handle(GetFoodsWithPagingQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<FoodDto> foods = await _repository.GetFoodsWithPaging(request.Page, request.PageSize);
            return foods.Select(x => new GetFoodsWithPagingQueryResult
            {
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                FoodId = x.FoodId,
                Price = x.Price
            }).ToList();
        }
    }
}