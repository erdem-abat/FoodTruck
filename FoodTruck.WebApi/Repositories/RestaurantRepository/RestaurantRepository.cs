﻿using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;
using FoodTruck.WebApi.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurant
    {
        private IMapper _mapper;
        private readonly UserIdentityDbContext _context;
        private readonly UserIdentityDbContext _identityContext;

        public RestaurantRepository(IMapper mapper, UserIdentityDbContext context, UserIdentityDbContext identityContext)
        {
            _mapper = mapper;
            _context = context;
            _identityContext = identityContext;
        }

        public async Task<RestaurantDto> CreateRestaurant(RestaurantDto restaurantDto)
        {
            try
            {
                var user = _identityContext.Users.First(x => x.Id == restaurantDto.UserId);

                if (user != null)
                {
                    var restaurant = _mapper.Map<Restaurant>(restaurantDto.RestaurantHeaderDto);

                    _context.Restaurant.Add(restaurant);
                    await _context.SaveChangesAsync();

                    foreach (var detailDto in restaurantDto.RestaurantDetailDto)
                    {
                        var detail = _mapper.Map<RestaurantDetail>(detailDto);

                        detail.RestaurantId = restaurant.RestaurantId;

                        _context.RestaurantDetails.Add(detail);
                    }

                    _context.RestaurantUsers.Add(new RestaurantUser
                    {
                        RestaurantId = restaurant.RestaurantId,
                        UserId = user.Id
                    });

                    await _context.SaveChangesAsync();

                    return restaurantDto;
                }

                return new RestaurantDto();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SeatPlanDto> CreateSeatPlan(SeatPlanDto seatPlanDto)
        {
            try
            {
                var table = _mapper.Map<Table>(seatPlanDto.TableDto);

                _context.Table.Add(table);
                await _context.SaveChangesAsync();


                foreach (var seatDto in seatPlanDto.SeatDtos)
                {
                    var seat = _mapper.Map<Seat>(seatDto);

                    seat.TableId = table.TableId;

                    _context.Seat.Add(seat);
                }

                await _context.SaveChangesAsync();

                return seatPlanDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> AddFoodToRestaurant(int foodId, int restaurantId, double price)
        {
            try
            {
                Food food = _context.Foods.First(x => x.FoodId == foodId);

                Restaurant restaurant = _context.Restaurant.First(x => x.RestaurantId == restaurantId);

                if (food != null && restaurant != null)
                {
                    FoodRestaurant foodRestaurant = await _context.FoodRestaurant.FirstOrDefaultAsync(x => x.FoodId == foodId && x.RestaurantId == restaurantId);

                    if (foodRestaurant != null)
                    {
                        foodRestaurant.Price = price != 0 ? price : foodRestaurant.Price;
                        _context.FoodRestaurant.Update(foodRestaurant);

                        await _context.SaveChangesAsync();

                        return "Food has been updated successfully.";
                    }
                    else
                    {
                        _context.FoodRestaurant.Add(new FoodRestaurant
                        {
                            FoodId = foodId,
                            RestaurantId = restaurantId,
                            Price = price != 0 ? price : foodRestaurant.Price
                        });

                        await _context.SaveChangesAsync();

                        return "Food has been added to restaurant successfully.";
                    }
                }

                return "There is an error.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ApproveRestaurant(bool isApprove, int restaurantId)
        {
            Restaurant restaurant = _context.Restaurant.First(x => x.RestaurantId == restaurantId);

            if (restaurant != null)
            {
                _context.Attach<Restaurant>(restaurant);
                _context.Entry<Restaurant>(restaurant).Property(x => x.IsApproved).IsModified = true;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}