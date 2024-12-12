using FoodTruck.Dto.CartDtos;

namespace FoodTruck.Application.Interfaces;

public interface ICartRepository
{
    Task<CartDto> CartUpsertAsync(CartDto cartDto);
    Task<FoodTruckCartsDto> FoodTruckCartUpsertAsync(FoodTruckCartsDto foodTruckCartsDto);
    Task<CartDto> GetCartAsync(string userId);
    Task<FoodTruckCartsDto> GetFoodTruckCartAsync(string userId);
    Task<bool> CartRemoveAsync(int cartDetailId);
    Task<bool> ApplyCouponAsync(string UserId, string couponCode);
}
