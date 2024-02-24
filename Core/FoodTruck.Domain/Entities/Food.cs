using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<FoodMood> FoodMoods { get; set; }
        public IEnumerable<FoodTaste> FoodTastes { get; set; }
    }
}