using FoodTruck.Application.Features.MediatR.Results.TasteResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.TasteQueries
{
    public class GetTasteQuery : IRequest<List<GetTasteQueryResults>>
    {
    }
}
