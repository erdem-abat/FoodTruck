using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Repositories.FoodRepository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodTruckContext _context;

        public FoodRepository(FoodTruckContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetFoodsWithCategory()
        {
            return await _context.Foods.Include(x => x.Category).ToListAsync();
        }
    }
}
