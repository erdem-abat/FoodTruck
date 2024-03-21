using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;

namespace FoodTruck.WebApi.Repositories.TruckRepository
{
    public class TruckRepository : ITruckRepository
    {
        private readonly FoodTruckContext _context;

        public TruckRepository(FoodTruckContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTruck(Truck truck)
        {
            try
            {
                if (truck != null)
                {
                    var checkTruck = _context.Trucks.Any(x => x.TruckName.ToLower() == truck.TruckName.ToLower());
                    if (!checkTruck)
                    {
                        await _context.Trucks.AddAsync(truck);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }

                var truckFromDb = _context.Trucks.First(x => x.TruckName.ToLower() == truck.TruckName.ToLower());

                foreach (var item in truck.Foods)
                {
                    _context.FoodTrucks.Add(new Domain.Entities.FoodTruck
                    {
                        FoodId = item.FoodId,
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