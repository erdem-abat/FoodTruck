namespace FoodTruck.Application.Features.MediateR.Results.FoodResults
{
    public class GetFoodsWithPagingQueryResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public string CategoryName { get; set; }
        public string TasteName { get; set; }
        public string MoodName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
