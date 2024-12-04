using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace FoodTruck.Application.Interfaces;

public interface IFoodRepository
{
    Task<List<Food>> GetFoodsWithCategoryAsync();
    Task<Food> CreateFoodAsync(Food food, List<int> foodMoodIds, List<int> foodTasteIds, List<int> ingredientIds);
    Task<List<FoodDto>> GetFoodsAsync();
    Task<List<FoodDto>> GetFoodsWithPagingAsync(int page, int pageSize);
    Task<FoodWithAllDto> GetFoodByIdAsync(int foodId, CancellationToken cancellationToken);
    Task<List<FoodWithAllDto>> GetFoodsWithAllAsync(CancellationToken cancellationToken);
    Task<List<FoodWithAllDto>> GetFoodsByFilterAsync(GetFoodsByFilterParameters getFoodsByFilterParameters);
    Task<string> DocumentUploadAsync(IFormFile formFile);
    Task<DataTable> FoodDataTableAsync(string path);
    Task<bool> ImportFoodAsync(DataTable food);
    Task<bool> ImportDataFromExcelAsync(Dictionary<string, DataTable> excelData, string userId, int restaurantId);
}
