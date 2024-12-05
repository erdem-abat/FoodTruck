using FoodTruck.Application.Features.MediatR.Queries.StatisticsQueries;
using FoodTruck.Application.Features.MediatR.Results.StatisticsResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.StatisticsHandlers
{
    public class GetTruckCountQueryHandler : IRequestHandler<GetTruckCountQuery, GetTruckCountQueryResult>
    {
        private readonly ITruckRepository _repository;

        public GetTruckCountQueryHandler(ITruckRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTruckCountQueryResult> Handle(GetTruckCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repository.GetTruckCountAsync();

            return new GetTruckCountQueryResult
            {
                TruckCount = count
            };
        }
    }
}
