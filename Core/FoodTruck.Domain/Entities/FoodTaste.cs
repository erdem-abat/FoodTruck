using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class FoodTaste
{
    public int FoodId { get; set; }
    [NotMapped]
    public virtual Food Food { get; set; }
    public int TasteId { get; set; }
    
    public virtual Taste Taste { get; set; }
}
