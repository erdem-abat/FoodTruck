using FoodTruck.Application.Features.MediatR.Results.MoodResults;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.MoodQueries
{
    public class GetMoodQuery : IRequest<List<GetMoodQueryResults>>
    {
    }
}
