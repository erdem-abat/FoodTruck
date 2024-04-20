using FoodTruck.Dto.FoodDtos;

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
