using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IRestaurant
    {
        Task<RestaurantDto> CreateRestaurant(RestaurantDto restaurantDto);
        Task<SeatPlanDto> CreateSeatPlan(SeatPlanDto seatPlanDto);
        Task<string> AddFoodToRestaurant(int foodId, int restaurantId, double price);
    }
}