namespace FoodTruck.Domain.Entities;

public class Seat
{
    public int SeatId { get; set; }
    public int TableId { get; set; }
    public virtual Table Table { get; set; }
    public bool IsAvailable { get; set; }
}