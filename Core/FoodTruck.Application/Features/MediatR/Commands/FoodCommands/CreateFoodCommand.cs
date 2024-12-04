using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodTruck.Application.Features.MediateR.Commands.FoodCommands
{
    public class CreateFoodCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public List<int> FoodTasteIds { get; set; }
        public List<int> FoodMoodIds { get; set; }
        public List<int> IngredientIds { get; set; }
    }
}
