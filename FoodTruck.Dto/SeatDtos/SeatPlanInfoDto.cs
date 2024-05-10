namespace FoodTruck.Dto.SeatDtos;

public class SeatPlanInfoDto
{
    public int? SeatId { get; set; }
    public int? TableId { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsSmoking { get; set; }
}