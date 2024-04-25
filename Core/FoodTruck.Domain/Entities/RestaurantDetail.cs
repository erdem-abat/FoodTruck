using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class RestaurantDetail
{
    public int RestaurantDetailId { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    public IEnumerable<Chef> Chefs { get; set; }
    public int LocationId { get; set; }
    [NotMapped]
    public Location Location { get; set; }
}