namespace FoodTruck.Domain.Entities;

public class Ingredient : DateFields
{
    public int IngredientId { get; set; }
    public string Name { get; set; }
    public string? smallImageUrl { get; set; }
    public string? bigImageUrl { get; set; }
    public double price { get; set; }
    public IEnumerable<Food> Foods { get; set; }
}
