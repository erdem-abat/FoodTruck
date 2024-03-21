using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Application.Features.MediatR.Results.CountryResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.CountryQueries
{
    public class GetCountryQuery : IRequest<List<GetCountryQueryResult>>
    {

    }
}
