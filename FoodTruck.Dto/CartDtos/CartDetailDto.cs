using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.CartDtos
{
    public class CartDetailDto
    {
        public int CartDetailId { get; set; }
        public int Count { get; set; }

        public int CartHeaderId { get; set; }
        public CartHeaderDto? CartHeader { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
