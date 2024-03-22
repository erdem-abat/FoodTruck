using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetFoodsWithCategory();
        Task<Food> CreateFood(Food food);
        Task<List<FoodDto>> GetFoods();
        Task<List<FoodWithAllDto>> GetFoodsWithAll();
        Task<List<FoodWithAllDto>> GetFoodsByFilter(GetFoodsByFilterParameters getFoodsByFilterParameters);
    }
}
