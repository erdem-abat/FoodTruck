using FoodTruck.Application.Features.MediatR.Queries.MoodQueries;
using FoodTruck.Application.Features.MediatR.Results.ChefResults;
using FoodTruck.Application.Features.MediatR.Results.MoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.MoodHandlers
{
    public class GetMoodQueryHandler : IRequestHandler<GetMoodQuery, List<GetMoodQueryResults>>
    {
        private readonly IRepository<Mood> _repository;

        public GetMoodQueryHandler(IRepository<Mood> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetMoodQueryResults>> Handle(GetMoodQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new GetMoodQueryResults
            {
               MoodId = x.MoodId,
               Name = x.Name,
            }).ToList();
        }
    }
}
