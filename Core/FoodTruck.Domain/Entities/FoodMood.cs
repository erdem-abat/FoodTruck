namespace FoodTruck.Domain.Entities
{
    public class FoodMood
    {
        public int FoodMoodId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int MoodId { get; set; }
        public Mood Mood { get; set; }
    }
}