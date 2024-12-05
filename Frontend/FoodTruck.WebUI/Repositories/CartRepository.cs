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

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = APIBase + "/api/Cart/ApplyCoupon"
            });
        }

        public Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = APIBase + "/api/Cart/FoodTruckCartUpsertAsync"
            });
        }
    }
}
