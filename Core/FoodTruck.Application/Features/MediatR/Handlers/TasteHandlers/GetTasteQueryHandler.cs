using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Features.MediatR.Queries.TasteQueries;
using FoodTruck.Application.Features.MediatR.Results.ChefResults;
using FoodTruck.Application.Features.MediatR.Results.TasteResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.TasteHandlers
{
    public class GetTasteQueryHandler : IRequestHandler<GetTasteQuery, List<GetTasteQueryResults>>
    {
        private readonly IRepository<Taste> _repository;

        public GetTasteQueryHandler(IRepository<Taste> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetTasteQueryResults>> Handle(GetTasteQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new GetTasteQueryResults
            {
               Name = x.Name,   
               TasteId = x.TasteId,
            }).ToList();
        }
    }
}
