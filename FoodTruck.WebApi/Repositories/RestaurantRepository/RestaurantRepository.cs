using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;
using FoodTruck.Dto.TruckReservationDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FoodTruck.WebApi.Repositories.RestaurantRepository
{
    public class RestaurantRepository : IRestaurant
    {
        private IMapper _mapper;
        private readonly FoodTruckContext _context;

        public RestaurantRepository(IMapper mapper, FoodTruckContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<RestaurantDto> CreateRestaurant(RestaurantDto restaurantDto)
        {
            try
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

                await _context.SaveChangesAsync();

                return restaurantDto;
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
                
               

                if(food != null && restaurant != null)
                {
                    FoodRestaurant foodRestaurant = await _context.FoodRestaurant.FirstOrDefaultAsync(x => x.FoodId == foodId && x.RestaurantId == restaurantId);

                    if(foodRestaurant!= null)
                    {
                        foodRestaurant.Price = price != 0 ? price: food.Price;
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
                            Price = price != 0 ? price : food.Price
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
    }
}