using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.CartDtos
{
    public class FoodTruckCartsDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<FoodTruckCartDetailDto>? FoodTruckCartDetail { get; set; }
    }
}