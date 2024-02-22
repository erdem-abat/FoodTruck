using FoodTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Data
{
    public class FoodTruckContext : DbContext
    {
        public FoodTruckContext(DbContextOptions<FoodTruckContext> options) : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
