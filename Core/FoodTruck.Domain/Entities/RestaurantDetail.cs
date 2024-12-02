using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class RestaurantDetail
{
    public int RestaurantDetailId { get; set; }
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public int LocationId { get; set; }
    [NotMapped]
    public virtual Location Location { get; set; }
}