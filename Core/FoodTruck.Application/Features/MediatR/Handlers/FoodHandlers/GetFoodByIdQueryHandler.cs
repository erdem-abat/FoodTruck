using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
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

            return new GetFoodByIdQueryResult
            {
                foodWithAllDto = await _repository.GetFoodById(request.Id)
            };
        }
    }
}
