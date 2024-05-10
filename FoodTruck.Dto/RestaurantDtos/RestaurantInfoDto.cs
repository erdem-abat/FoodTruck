namespace FoodTruck.Dto.RestaurantDtos;

public class RestaurantInfoDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public bool IsAlcohol { get; set; }
    public int RestaurantDetailId { get; set; }
    public int LocationId { get; set; }
    public string locationName { get; set; }
    public int? SeatId { get; set; }
    public int? TableId { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsSmoking { get; set; }
}