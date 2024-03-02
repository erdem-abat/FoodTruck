namespace FoodTruck.Dto.CartDtos
{
    public class CartsDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartDetailDto>? CartDetails { get; set; }
    }
}