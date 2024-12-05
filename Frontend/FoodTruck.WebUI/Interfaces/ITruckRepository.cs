using FoodTruck.WebUI.Models;

namespace FoodTruck.WebUI.Interfaces
{
    public interface ITruckRepository
    {
        Task<ResponseDto?> GetTruckCountAsync();
    }
}
