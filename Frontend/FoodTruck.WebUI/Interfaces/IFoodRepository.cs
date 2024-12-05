using FoodTruck.WebUI.Models;

namespace FoodTruck.WebUI.Interfaces
{
    public interface IFoodRepository
    {
        Task<ResponseDto?> GetAllFoodsAsync();
        Task<ResponseDto?> GetFoodByIdAsync(int productId);
    }
}
