using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CouponDtos;

namespace FoodTruck.Application.Interfaces;

public interface ICouponRepository
{
    Task<CouponDto> CreateCouponAsync(Coupon coupon);
    Task<CouponDto> UpdateCouponAsync(Coupon coupon);
    Task<bool> DeleteCouponAsync(int id);
}
