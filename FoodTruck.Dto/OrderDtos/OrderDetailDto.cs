using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Dto.OrderDtos
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
        public double Price { get; set; }
    }
}
