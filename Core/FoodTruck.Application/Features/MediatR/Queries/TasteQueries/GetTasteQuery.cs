using FoodTruck.Application.Features.MediatR.Results.TasteResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.TasteQueries
{
    public class GetTasteQuery : IRequest<List<GetTasteQueryResults>>
    {
    }
}
