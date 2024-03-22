using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities
{
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<FoodMood> Foods { get; set; }
    }
}