namespace FoodTruck.Domain.Entities;

public class FoodMood
{
    public int FoodId { get; set; }
    public virtual Food Food { get; set; }
    public int MoodId { get; set; }
    public virtual Mood Mood { get; set; }
}