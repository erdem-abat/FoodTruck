namespace FoodTruck.Dto.FoodDtos
{
    public class FoodDto
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }

        public int CountryId { get; set; }
        public int CategoryId { get; set; }
    }
}
