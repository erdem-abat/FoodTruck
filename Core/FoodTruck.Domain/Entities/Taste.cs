using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities
{
    public class Taste
    {
        [Key]
        public int TasteId { get; set; }
        [Required]
        public string Name { get; set; }      
        public IEnumerable<Food> Foods { get; set; }
    }
}
