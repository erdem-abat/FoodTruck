using FoodTruck.WebUI.Models;

namespace FoodTruck.WebUI.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
