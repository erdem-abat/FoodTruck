using FoodTruck.Application.Features.MediateR.Queries.LocationQueries;
using FoodTruck.Application.Features.MediatR.Results.LocationResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.LocationHandlers;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, List<GetLocationQueryResult>>
{
    private readonly IRestaurant _repository;

    public GetLocationQueryHandler(IRestaurant repository)
    {
        _repository = repository;
    }

    public async Task<List<GetLocationQueryResult>> Handle(GetLocationQuery request,
        CancellationToken cancellationToken)
    {
        var values = await _repository.GetLocations();
        return values.Select(x => new GetLocationQueryResult
        {
            Latitude = x.Latitude,
            Name = x.Name,
            Longitude = x.Longitude,
            LocationId = x.LocationId
        }).ToList();
    }
}