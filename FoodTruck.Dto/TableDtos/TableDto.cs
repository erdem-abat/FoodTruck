using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Dto.TableDtos
{
    public class TableDto
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public IEnumerable<SeatDto> SeatDtos { get; set; }
        public bool IsSmoking { get; set; }
    }
}