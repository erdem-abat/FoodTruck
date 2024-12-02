using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class Mood
{
    [Key]
    public int MoodId { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual IEnumerable<FoodMood> Foods { get; set; }
}