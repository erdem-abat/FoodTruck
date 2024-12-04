using FoodTruck.Application.Features.MediatR.Results.CountryResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CountryQueries
{
    public class GetCountryQuery : IRequest<List<GetCountryQueryResult>>
    {

    }
}
