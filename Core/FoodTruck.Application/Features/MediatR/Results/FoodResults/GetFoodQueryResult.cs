namespace FoodTruck.Application.Features.MediateR.Results.FoodResults
{
    public class GetFoodQueryResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public string CategoryName { get; set; }
        public List<string> TasteNames { get; set; }
        public List<string> MoodNames { get; set; }
        public List<string> IngredientNames { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
