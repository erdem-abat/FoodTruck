namespace FoodTruck.Domain.Entities
{
    public class FoodChef
    {
        public int FoodChefId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int ChefId { get; set; }
        public Chef Chef { get; set; }
    }
}