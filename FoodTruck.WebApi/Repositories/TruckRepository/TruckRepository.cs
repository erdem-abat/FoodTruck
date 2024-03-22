using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;
using System.Linq;
using ZstdSharp.Unsafe;

namespace FoodTruck.WebApi.Repositories.TruckRepository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly FoodTruckContext _context;

        public TruckRepository(FoodTruckContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTruck(Truck truck, List<int> foodIds, List<int> chefIds)
        {
            try
            {
                if (truck.TruckName != null)
                {
                    var checkTruck = _context.Trucks.Any(x => x.TruckName.ToLower() == truck.TruckName.ToLower());
                    if (!checkTruck)
                    {
                        Truck truckData = new Truck
                        {
                            TruckName = truck.TruckName
                        };
                        await _context.Trucks.AddAsync(truckData);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }

                var truckFromDb = _context.Trucks.First(x => x.TruckName.ToLower() == truck.TruckName.ToLower());

                var list = _context.Chefs.Where(x => chefIds.Contains(x.ChefId)).ToList();

                foreach (var chef in list)
                {
                    chef.TruckId = truckFromDb.TruckId;
                    _context.Chefs.Update(chef);
                }

                await _context.SaveChangesAsync();

                foreach (var item in foodIds)
                {
                    _context.FoodTrucks.Add(new Domain.Entities.FoodTruck
                    {
                        FoodId = item,
                        TruckId = truckFromDb.TruckId,
                    });
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}