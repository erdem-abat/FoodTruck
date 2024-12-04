using FoodTruck.Application.Features.MediatR.Queries.CountryQueries;
using FoodTruck.Application.Features.MediatR.Results.CountryResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CountryHandlers
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, List<GetCountryQueryResult>>
    {
        private readonly IRepository<Country> _repository;

        public GetCountryQueryHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCountryQueryResult>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Country> values = await _repository.GetAllAsync();
            return values.Select(x => new GetCountryQueryResult
            {
                Name = x.Name,
                CountryId = x.CountryId
            }).ToList();
        }
    }
}
