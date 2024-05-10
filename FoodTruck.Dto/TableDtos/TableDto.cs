using System.ComponentModel.DataAnnotations.Schema;
using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Dto.TableDtos
{
    public class TableDto
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public bool IsSmoking { get; set; }
    }
}