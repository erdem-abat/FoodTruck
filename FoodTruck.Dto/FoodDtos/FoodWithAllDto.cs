using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.FoodDtos
{
    public class FoodWithAllDto
    {
        public Food Food { get; set; }
        public FoodTaste? FoodTaste { get; set; }
        public FoodMood? FoodMood { get; set; }
        public Category Category { get; set; }
        public Country Country { get; set; }
        public Mood? Mood { get; set; }
        public Taste? Taste { get; set; }
    }
}
