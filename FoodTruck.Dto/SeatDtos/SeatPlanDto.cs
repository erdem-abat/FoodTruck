using FoodTruck.Dto.TableDtos;

namespace FoodTruck.Dto.SeatDtos;

public class SeatPlanDto
{
    public IEnumerable<SeatDto> SeatDtos { get; set; }
    public TableDto TableDto { get; set; }
}
