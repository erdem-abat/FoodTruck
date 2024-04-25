namespace FoodTruck.Dto.SeatDtos;

public class SeatDto
{
    public int SeatId { get; set; }
    public int TableId { get; set; }
    public bool IsAvailable { get; set; }
}