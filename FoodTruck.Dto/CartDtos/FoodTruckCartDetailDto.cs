using FoodTruck.Dto.FoodDtos;
using FoodTruck.Dto.TruckDtos;

namespace FoodTruck.Dto.CartDtos
{
    public class FoodTruckCartDetailDto
    {
        public int FoodTruckCartDetailId { get; set; }
        public int Count { get; set; }

        public int CartHeaderId { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
        public int TruckId { get; set; }
    }
}
