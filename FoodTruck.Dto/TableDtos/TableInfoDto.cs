using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Dto.TableDtos;

public class TableInfoDto
{
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
    public bool IsSmoking { get; set; }
    public List<SeatInfoDto> SeatInfoDtos { get; set; }
}