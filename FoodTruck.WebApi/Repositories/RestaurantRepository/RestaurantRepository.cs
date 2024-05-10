using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.LocationDtos;
using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.SeatDtos;
using FoodTruck.Dto.TableDtos;
using FoodTruck.Dto.TruckReservationDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Location = Stripe.Terminal.Location;

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

        public async Task<(List<RestaurantInfoDto> Restaurants, int totalTableCount, int availableTableCount)>
            GetRestaurants()
        {
            try
            {
                var values = await (from rest in _context.Restaurant
                    join restDet in _context.RestaurantDetails on rest.RestaurantId equals restDet.RestaurantId
                    join loc in _context.Locations on restDet.LocationId equals loc.LocationId
                    join tbl in _context.Table on rest.RestaurantId equals tbl.RestaurantId into tableGroup
                    select new RestaurantInfoDto
                    {
                        RestaurantId = rest.RestaurantId,
                        LocationId = restDet.LocationId,
                        RestaurantDetailId = restDet.RestaurantDetailId,
                        CloseTime = rest.CloseTime,
                        IsAlcohol = rest.IsAlcohol,
                        OpenTime = rest.OpenTime,
                        RestaurantName = rest.RestaurantName,
                        locationName = loc.Name,
                        TableInfoDtos = tableGroup.Select(t => new TableInfoDto
                        {
                            TableId = t.TableId,
                            IsSmoking = t.IsSmoking,
                            RestaurantId = t.RestaurantId,
                            SeatInfoDtos = (from st in _context.Seat
                                where st.TableId == t.TableId
                                select new SeatInfoDto
                                {
                                    SeatId = st.SeatId,
                                    IsAvailable = st.IsAvailable,
                                    TableId = st.TableId
                                }).ToList()
                        }).ToList()
                    }).ToListAsync();

                List<(int RestaurantId, double SolidityRatio)> solidityRatios =
                    new List<(int RestaurantId, double SolidityRatio)>();

                int totalTables = 0;
                int availableTables = 0;
                foreach (var restaurant in values)
                {
                    bool availableSeat = false;

                    foreach (var table in restaurant.TableInfoDtos)
                    {
                        availableSeat = table.SeatInfoDtos.Any(seat => seat.IsAvailable == false);
                        totalTables += 1;
                        availableTables += availableSeat == true ? 0 : 1;
                    }
                }

                return (values, totalTables, availableTables);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RestaurantInfoDto> GetRestaurantById(int restaurantId)
        {
            try
            {
                var values = await (from rest in _context.Restaurant
                    join restDet in _context.RestaurantDetails on rest.RestaurantId equals restDet.RestaurantId
                    join loc in _context.Locations on restDet.LocationId equals loc.LocationId
                    join tbl in _context.Table on rest.RestaurantId equals tbl.RestaurantId into tableGroup
                    where rest.RestaurantId == restaurantId
                    select new RestaurantInfoDto
                    {
                        RestaurantId = rest.RestaurantId,
                        LocationId = restDet.LocationId,
                        RestaurantDetailId = restDet.RestaurantDetailId,
                        CloseTime = rest.CloseTime,
                        IsAlcohol = rest.IsAlcohol,
                        OpenTime = rest.OpenTime,
                        RestaurantName = rest.RestaurantName,
                        locationName = loc.Name,
                        TableInfoDtos = tableGroup.Select(t => new TableInfoDto
                        {
                            TableId = t.TableId,
                            IsSmoking = t.IsSmoking,
                            RestaurantId = t.RestaurantId,
                            SeatInfoDtos = (from st in _context.Seat
                                where st.TableId == t.TableId
                                select new SeatInfoDto
                                {
                                    SeatId = st.SeatId,
                                    IsAvailable = st.IsAvailable,
                                    TableId = st.TableId
                                }).ToList()
                        }).ToList()
                    }).FirstOrDefaultAsync();

                return values;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LocationDto>> GetLocations()
        {
            try
            {
                var values = await _context.Locations.ToListAsync();
                return _mapper.Map<List<LocationDto>>(values);
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
                    FoodRestaurant foodRestaurant =
                        await _context.FoodRestaurant.FirstOrDefaultAsync(x =>
                            x.FoodId == foodId && x.RestaurantId == restaurantId);

                    if (foodRestaurant != null)
                    {
                        foodRestaurant.Price = price != 0 ? price : food.Price;
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