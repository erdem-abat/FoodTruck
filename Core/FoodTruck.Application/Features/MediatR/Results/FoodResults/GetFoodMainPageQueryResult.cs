namespace FoodTruck.Application.Features.MediateR.Results.FoodResults
{
    public class GetFoodMainPageQueryResult
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}