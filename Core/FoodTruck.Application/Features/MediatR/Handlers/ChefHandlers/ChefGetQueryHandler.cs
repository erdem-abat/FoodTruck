using FoodTruck.Application.Features.MediatR.Queries.ChefQueries;
using FoodTruck.Application.Features.MediatR.Results.ChefResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.ChefHandlers
{
    public class ChefGetQueryHandler : IRequestHandler<GetChefQuery, List<GetChefQueryResult>>
    {
        private readonly IRepository<Chef> _repository;

        public ChefGetQueryHandler(IRepository<Chef> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetChefQueryResult>> Handle(GetChefQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new GetChefQueryResult
            {
                ChefId = x.ChefId,
                Name = x.Name,
                Popularity = x.Popularity,
                TruckId = x.TruckId
            }).ToList();
        }
    }
}
