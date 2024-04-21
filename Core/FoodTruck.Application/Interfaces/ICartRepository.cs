using FoodTruck.Dto.AuthDtos;
using FoodTruck.Dto.CartDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<CartsDto> CartUpsert(CartsDto cartsDto);
        Task<CartsDto> GetCart(string userId);
        Task<bool> CartRemove(int cartDetailId);
        Task<bool> ApplyCoupon(string UserId, string couponCode);
    }
}
