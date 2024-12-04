using FoodTruck.Domain.Entities;
using FoodTruck.Dto.TruckDtos;

namespace FoodTruck.Application.Interfaces;

public interface ITruckRepository
{
    Task<bool> CreateTruckAsync(Truck truck, List<int[][]> foodIdsWithStocks, List<int> chefIds);
    Task<List<FoodTruckDto>> GetFoodTrucksAsync();
}
