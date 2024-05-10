using FoodTruck.Dto.LocationDtos;
using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IRestaurant
    {
        Task<RestaurantDto> CreateRestaurant(RestaurantDto restaurantDto);
        Task<(List<RestaurantInfoDto> Restaurants, int totalTableCount, int availableTableCount)> GetRestaurants();
        Task<RestaurantInfoDto> GetRestaurantById(int restaurantId);
        Task<List<LocationDto>> GetLocations();
        Task<SeatPlanDto> CreateSeatPlan(SeatPlanDto seatPlanDto);
        Task<string> AddFoodToRestaurant(int foodId, int restaurantId, double price);
    }
}