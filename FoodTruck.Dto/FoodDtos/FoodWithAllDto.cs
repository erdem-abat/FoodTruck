using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.FoodDtos
{
    public class FoodWithAllDto
    {
        public Food? Food { get; set; }
        public FoodTaste? FoodTaste { get; set; }
        public FoodMood? FoodMood { get; set; }
        public FoodIngredient? FoodIngredient { get; set; }
        public Category? Category { get; set; }
        public Country? Country { get; set; }
        public List<Mood?> Moods { get; set; }
        public List<Taste?> Tastes { get; set; }
        public List<Ingredient?> Ingredients { get; set; }
    }
}