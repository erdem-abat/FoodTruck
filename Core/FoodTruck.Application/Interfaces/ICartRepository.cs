using FoodTruck.Dto.CartDtos;

namespace FoodTruck.Application.Interfaces;

public interface ICartRepository
{
    Task<CartsDto> CartUpsertAsync(CartsDto cartsDto);
    Task<FoodTruckCartsDto> FoodTruckCartUpsertAsync(FoodTruckCartsDto foodTruckCartsDto);
    Task<CartsDto> GetCartAsync(string userId);
    Task<FoodTruckCartsDto> GetFoodTruckCartAsync(string userId);
    Task<bool> CartRemoveAsync(int cartDetailId);
    Task<bool> ApplyCouponAsync(string UserId, string couponCode);
}
