using FoodTruck.Application.Features.MediatR.Results.LocationResults;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Queries.LocationQueries;

public class GetLocationQuery :  IRequest<List<GetLocationQueryResult>>
{
    
}