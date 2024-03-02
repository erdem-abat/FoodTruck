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
        public virtual Country Country { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<FoodMood> FoodMoods { get; set; }
        public virtual IEnumerable<FoodTaste> FoodTastes { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}