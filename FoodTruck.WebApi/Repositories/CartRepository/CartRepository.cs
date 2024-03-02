using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Repositories.CartRepository
{
    public class CartRepository : ICartRepository
    {
        private readonly FoodTruckContext _dbContext;
        private IMapper _mapper;

        public CartRepository(FoodTruckContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
                        _dbContext.CartDetails.Add(_mapper.Map<CartDetail>(cartsDto.CartDetails.First()));
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
    }
}
