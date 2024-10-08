﻿using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.FoodDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediateR.Handlers.FoodHandlers
{
    public class GetFoodsByFilterQueryHandler : IRequestHandler<GetFoodsByFilterQuery, List<GetFoodsByFilterQueryResult>>
    {
        private readonly IFoodRepository _repository;

        public GetFoodsByFilterQueryHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodsByFilterQueryResult>> Handle(GetFoodsByFilterQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<FoodWithAllDto> foods = await _repository.GetFoodsByFilter(request.getFoodsByFilterParameters);
            return foods.Select(x => new GetFoodsByFilterQueryResult
            {
                CountryName = x.Country.Name,
                Description = x.Food.Description,
                ImageLocalPath = x.Food.ImageLocalPath,
                CategoryName = x.Category.CategoryName,
                ImageUrl = x.Food.ImageUrl,
                Name = x.Food.Name,
                Price = x.Food.Price,
                FoodId = x.Food.FoodId,
                MoodName = x.Mood.Name,
                TasteName = x.Taste.Name
            }).ToList();
        }
    }
}
