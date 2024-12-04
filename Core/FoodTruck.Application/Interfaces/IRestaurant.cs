using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Application.Interfaces;

public interface IRestaurant
{
    Task<RestaurantDto> CreateRestaurantAsync(RestaurantDto restaurantDto);
    Task<SeatPlanDto> CreateSeatPlanAsync(SeatPlanDto seatPlanDto);
    Task<string> AddFoodToRestaurantAsync(int foodId, int restaurantId, double price);
    Task<bool> ApproveRestaurantAsync(bool isApprove, int restaurantId);
}