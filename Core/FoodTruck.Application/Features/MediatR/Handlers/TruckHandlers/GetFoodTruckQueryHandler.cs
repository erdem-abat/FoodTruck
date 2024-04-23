using FoodTruck.Application.Features.MediatR.Queries.TruckQuerires;
using FoodTruck.Application.Features.MediatR.Results.TruckResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.TruckHandlers
{
    public class GetFoodTruckQueryHandler : IRequestHandler<GetFoodTruckQuery, List<GetFoodTruckQueryResult>>
    {
        private readonly ITruckRepository _truckRepository;

        public GetFoodTruckQueryHandler(ITruckRepository truckRepository)
        {
            _truckRepository = truckRepository;
        }
        public async Task<List<GetFoodTruckQueryResult>> Handle(GetFoodTruckQuery request, CancellationToken cancellationToken)
        {
            var values = await _truckRepository.GetFoodTrucks();

            return values.Select(x => new GetFoodTruckQueryResult
            {
                FoodTruckDto = x
            }).ToList();
        }
    }
}
