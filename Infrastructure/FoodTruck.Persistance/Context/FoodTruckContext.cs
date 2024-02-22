using FoodTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.Persistence.Context
{
    public class FoodTruckContext 
    {
        //public FoodTruckContext(DbContextOptions<FoodTruckContext> options) : base(options)
        //{
        //}

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}