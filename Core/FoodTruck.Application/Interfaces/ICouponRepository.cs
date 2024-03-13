using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CouponDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDto> CreateCoupon(Coupon coupon);
        Task<CouponDto> UpdateCoupon(Coupon coupon);
        Task<CouponDto> DeleteCoupon(int id);
    }
}
