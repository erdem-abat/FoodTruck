using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
