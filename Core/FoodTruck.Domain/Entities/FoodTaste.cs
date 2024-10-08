using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodTaste
{
    public int FoodTasteId { get; set; }
    public int FoodId { get; set; }
    [NotMapped]
    public Food Food { get; set; }
    public int TasteId { get; set; }
    [NotMapped]
    public Taste Taste { get; set; }
}
