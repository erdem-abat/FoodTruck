using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities;

public class Rate : DateFields
{
    public int RateId { get; set; }
    [Range(1, 10)]
    public double Rating { get; set; }
    public IEnumerable<FoodRate> FoodRates { get; set; }
}
