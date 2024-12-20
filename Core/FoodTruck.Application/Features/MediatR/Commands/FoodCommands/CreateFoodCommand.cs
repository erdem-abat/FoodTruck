﻿using Amazon.Util.Internal;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodTruck.Application.Features.MediateR.Commands.FoodCommands
{
    public class CreateFoodCommand : IRequest
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public IFormFile Image { get; set; }
        public List<int> FoodTasteIds { get; set; }
        public List<int> FoodMoodIds { get; set; }
    }
}
