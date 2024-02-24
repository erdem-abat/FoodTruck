using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<Food> Foods { get; set; }
    }
}
