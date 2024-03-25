using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Features.MediatR.Results.MoodResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.MoodQueries
{
    public class GetMoodQuery : IRequest<List<GetMoodQueryResults>>
    {
    }
}
