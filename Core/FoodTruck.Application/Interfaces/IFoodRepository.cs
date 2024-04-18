using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace FoodTruck.Application.Interfaces
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetFoodsWithCategory();
        Task<Food> CreateFood(Food food, List<int> foodMoodIds, List<int> foodTasteIds);
        Task<List<FoodDto>> GetFoods();
        Task<List<FoodDto>> GetFoodsWithPaging(int page, int pageSize);
        Task<List<FoodWithAllDto>> GetFoodsWithAll();
        Task<List<FoodWithAllDto>> GetFoodsByFilter(GetFoodsByFilterParameters getFoodsByFilterParameters);
        Task<string> DocumentUpload(IFormFile formFile);
        Task<DataTable> FoodDataTable(string path);
        Task<bool> ImportFood(DataTable food);
    }
}
