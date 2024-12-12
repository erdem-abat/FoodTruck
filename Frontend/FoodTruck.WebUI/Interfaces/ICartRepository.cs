using FoodTruck.WebUI.Models;

namespace FoodTruck.WebUI.Interfaces
{
    public interface ICartRepository
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailId);
        Task<ResponseDto?> ApplyCouponAsync(string userId, string couponCode);
        Task<ResponseDto?> EmailCart(CartDto cartDto);
    }
}
