namespace FoodTruck.Application.Features.MediatR.Results.CategoryResults
{
    public class GetCategoryQueryResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsValid { get; set; }
    }
}
