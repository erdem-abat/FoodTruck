namespace FoodTruck.Application.Features.MediatR.Results.IngredientResults
{
    public class GetIngredientQueryResult
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string? smallImageUrl { get; set; }
        public string? bigImageUrl { get; set; }
        public double price { get; set; }
    }
}
