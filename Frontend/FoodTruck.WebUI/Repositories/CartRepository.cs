using FoodTruck.WebUI.Interfaces;
using FoodTruck.WebUI.Models;
using FoodTruck.WebUI.Services.IServices;
using static FoodTruck.WebUI.Utility.SD;

namespace FoodTruck.WebUI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IBaseService _baseService;

        public CartRepository(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(string userId, string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = $"{APIBase}/api/Cart/ApplyCoupon?userId={Uri.EscapeDataString(userId)}&couponCode={Uri.EscapeDataString(couponCode)}"
            });
        }

        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = APIBase + "/api/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = APIBase + "/api/Cart/GetCart/" + userId
            });
        }

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Url = $"{APIBase}/api/Cart/RemoveCart?cartDetailId={cartDetailId}"
            });
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = APIBase + "/api/Cart/CartUpsert"
            });
        }
    }
}