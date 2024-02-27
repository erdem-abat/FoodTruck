namespace FoodTruck.Dto.FoodDtos
{
    public class GetFoodsByFilterParameters
    {
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
        public int? categoryId { get; set; }
        public int? countryId { get; set; }
        public int? tasteId { get; set; }
        public int? moodId { get; set; }
    }
}