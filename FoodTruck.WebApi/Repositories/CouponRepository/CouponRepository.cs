using AutoMapper;
using Azure;
using FoodTruck.Application.Exceptions;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CouponDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Repositories.CouponRepository
{
    public class CouponRepository : ICouponRepository
    {
        private IMapper _mapper;
        private UserIdentityDbContext _context;
        public CouponRepository(IMapper mapper, UserIdentityDbContext dbcontext)
        {
            _mapper = mapper;
            _context = dbcontext;
        }

        public async Task<CouponDto> CreateCouponAsync(Coupon coupon)
        {
            try
            {
                Coupon obj = coupon;
                _context.Coupons.Add(obj);

                CouponDto couponDto = _mapper.Map<CouponDto>(coupon);

                var options = new Stripe.CouponCreateOptions
                {
                    DurationInMonths = 3,
                    Duration = "repeating",
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode
                };
                var service = new Stripe.CouponService();
                service.Create(options);

                await _context.SaveChangesAsync();
                
                return couponDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var coupon = _context.Coupons.First(x => x.CouponId == id);

            if (coupon == null)
            {
                throw new NotFoundException("Coupon not found!");
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CouponDto> UpdateCouponAsync(Coupon coupon)
        {
            var couponFromDb = _context.Coupons.First(x => x.CouponId == coupon.CouponId);

            if (couponFromDb == null)
            {
                throw new NotFoundException("Coupon not found!");
            }

            _context.Attach(coupon);
            _context.Entry(coupon).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _mapper.Map<CouponDto>(coupon);
        }
    }
}
