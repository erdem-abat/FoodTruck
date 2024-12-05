using System.ComponentModel.DataAnnotations;

namespace FoodTruck.WebUI.Models
{
    public class FoodDto
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        [Range(1, 100)]
        public int Count { get; set; } = 1;
        
        public int CountryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
