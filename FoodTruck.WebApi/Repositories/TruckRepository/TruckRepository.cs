using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.Dto.TruckDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Repositories.TruckRepository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly UserIdentityDbContext _context;
        private IMapper _mapper;

        public TruckRepository(UserIdentityDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> CreateTruckAsync(Truck truck, List<int[][]> foodIdsWithStocks, List<int> chefIds)
        {
            try
            {
                if (truck.TruckName != null)
                {
                    var checkTruck = _context.Trucks.Any(x => x.TruckName.ToLower() == truck.TruckName.ToLower());
                    if (checkTruck)
                    {
                        return false;
                    }
                    else
                    {
                        Truck truckData = new Truck
                        {
                            TruckName = truck.TruckName
                        };
                        await _context.Trucks.AddAsync(truckData);
                        await _context.SaveChangesAsync();
                    }
                }

                var truckFromDb = _context.Trucks.First(x => x.TruckName.ToLower() == truck.TruckName.ToLower());

                var list = _context.Chefs.Where(x => chefIds.Contains(x.ChefId) && x.TruckId == 1).ToList();

                foreach (var chef in list)
                {
                    chef.TruckId = truckFromDb.TruckId;
                    _context.Chefs.Update(chef);
                }

                await _context.SaveChangesAsync();

                foreach (var item in foodIdsWithStocks)
                {

                    for (int i = 0; i < item.Length; ++i)
                    {
                        var data = new Domain.Entities.FoodTruck
                        {
                            FoodId = item[i][0],
                            TruckId = truckFromDb.TruckId,
                            Stock = item[i][1]
                        };

                        _context.FoodTrucks.Add(data);
                    }
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<FoodTruckDto>> GetFoodTrucksAsync()
        {
            try
            {
                //return _context.FoodTrucks.Select(x => new FoodTruckDto
                //{
                //    FoodId = x.FoodId,
                //    Food = _mapper.Map<FoodDto>(_context.Foods.First(y => y.FoodId == x.FoodId)),
                //    Stock = x.Stock,
                //    Truck = _mapper.Map<TruckDto>(_context.Trucks.First(y => y.TruckId == x.TruckId)),
                //    TruckId = x.TruckId,
                //    FoodTruckId = x.FoodTruckId
                //}).ToList();

                return await (from foodtruck in _context.FoodTrucks
                              join truck in _context.Trucks on foodtruck.TruckId equals truck.TruckId
                              join food in _context.Foods on foodtruck.FoodId equals food.FoodId
                              select new FoodTruckDto
                              {
                                  Food = _mapper.Map<FoodDto>(food),
                                  Truck = _mapper.Map<TruckDto>(truck),
                                  FoodId = foodtruck.FoodId,
                                  Stock = foodtruck.Stock,
                                  TruckId = foodtruck.TruckId
                              }).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetTruckCountAsync()
        {
            int count = await _context.Trucks.CountAsync();
            return count;
        }
    }
}