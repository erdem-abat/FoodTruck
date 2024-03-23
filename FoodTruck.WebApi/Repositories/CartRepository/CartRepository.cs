using AutoMapper;
using Azure;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FoodTruck.WebApi.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly FoodTruckContext _dbContext;
        private IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly IRepository<Coupon> _couponRepository;

        public CartRepository(FoodTruckContext dbContext, IMapper mapper, IFoodRepository foodRepository, IRepository<Coupon> couponRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _foodRepository = foodRepository;
            _couponRepository = couponRepository;
        }

        public async Task<CartsDto> CartUpsert(CartsDto cartsDto)
        {
            try
            {
                var cartHeaderFromDb = await _dbContext.CartHeaders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == cartsDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartsDto.CartHeader);
                    _dbContext.CartHeaders.Add(cartHeader);
                    await _dbContext.SaveChangesAsync();
                    cartsDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _dbContext.CartDetails.Add(_mapper.Map<CartDetail>(cartsDto.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        x => x.FoodId == cartsDto.CartDetails.First().FoodId &&
                        x.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        cartsDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        var data = _mapper.Map<CartDetail>(cartsDto.CartDetails.First());
                        _dbContext.CartDetails.Add(data);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        cartsDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartsDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartsDto.CartDetails.First().CartDetailId = cartDetailsFromDb.CartDetailId;
                        _dbContext.CartDetails.Update(_mapper.Map<CartDetail>(cartsDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();
                    }
                }
                return cartsDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CartsDto> GetCart(string userId)
        {
            try
            {
                CartsDto cartDto = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(_dbContext.CartHeaders.First(x => x.UserId == userId))
                };
                cartDto.CartDetails = _mapper.Map<IEnumerable<CartDetailDto>>(_dbContext.CartDetails
                    .Where(x => x.CartHeaderId == cartDto.CartHeader.CartHeaderId));

                IEnumerable<FoodDto> foodDto = await _foodRepository.GetFoods();

                foreach (var item in cartDto.CartDetails)
                {
                    item.Food = foodDto.FirstOrDefault(x => x.FoodId == item.FoodId);
                    cartDto.CartHeader.CartTotal += (item.Count * item.Food.Price);
                }

                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    Coupon? coupon = await _couponRepository.GetByFilterAsync(x => x.CouponCode == cartDto.CartHeader.CouponCode);
                    if (coupon != null && cartDto.CartHeader.CartTotal > coupon.minAmount)
                    {
                        cartDto.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cartDto.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                return cartDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ApplyCoupon(CartsDto cartsDto)
        {
            try
            {
                var cartFromDb = await _dbContext.CartHeaders.FirstAsync(x => x.UserId == cartsDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartsDto.CartHeader.CouponCode;
                _dbContext.CartHeaders.Update(cartFromDb);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CartRemove(int cartDetailId)
        {
            try
            {
                CartDetail cartDetails = await _dbContext.CartDetails
                                         .FirstAsync(x => x.CartDetailId == cartDetailId);

                int totalCountofCartItem = await _dbContext.CartDetails.Where(x => x.CartHeaderId == cartDetails.CartHeaderId).CountAsync();

                _dbContext.CartDetails.Remove(cartDetails);

                if (totalCountofCartItem == 1)
                {
                    var cartHeaderToRemove = await _dbContext.CartHeaders
                         .FirstAsync(x => x.CartHeaderId == cartDetails.CartHeaderId);

                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}