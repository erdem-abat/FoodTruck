using FoodTruck.Dto.FoodDtos;

namespace FoodTruck.Dto.TruckDtos
{
    public class FoodTruckDto
    {
        public int FoodTruckId { get; set; }
        public int FoodId { get; set; }
        public FoodDto Food { get; set; }
        public int TruckId { get; set; }
        public TruckDto Truck { get; set; }
        public int Stock { get; set; }
    }
}
