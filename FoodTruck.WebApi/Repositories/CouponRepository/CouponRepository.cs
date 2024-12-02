using AutoMapper;
using Azure;
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

        public async Task<CouponDto> CreateCoupon(Coupon coupon)
        {
            try
            {
                Coupon obj = coupon;
                _context.Coupons.Add(obj);
                await _context.SaveChangesAsync();

                //var options = new Stripe.CouponCreateOptions
                //{
                //    DurationInMonths = 3,
                //    Duration = "repeating",
                //    AmountOff = (long)(couponDto.DiscountAmount * 100),
                //    Name = couponDto.CouponCode,
                //    Currency = "usd",
                //    Id = couponDto.CouponCode
                //};
                //var service = new Stripe.CouponService();
                //service.Create(options);

                return _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<CouponDto> DeleteCoupon(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CouponDto> UpdateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
