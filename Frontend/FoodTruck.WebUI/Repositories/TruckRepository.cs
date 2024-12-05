using FoodTruck.WebUI.Models;
using FoodTruck.WebUI.Services.IServices;
using static FoodTruck.WebUI.Utility.SD;
using FoodTruck.WebUI.Interfaces;

namespace FoodTruck.WebUI.Repositories
{
    public class TruckRepository : ITruckRepository
    {
        private readonly IBaseService _baseService;

        public TruckRepository(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> GetTruckCountAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = APIBase + "/api/Statistics/GetTruckCount"
            });
        }
    }
}
