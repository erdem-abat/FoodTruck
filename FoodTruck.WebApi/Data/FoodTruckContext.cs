using FoodTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Data
{
    public class FoodTruckContext : DbContext
    {
        public FoodTruckContext(DbContextOptions<FoodTruckContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet(null, DelegationModes.ApplyToDatabases);
        }
        
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Taste> Tastes { get; set; }
        public DbSet<FoodTaste> FoodTaste { get; set; }
        public DbSet<FoodMood> FoodMood { get; set; }

    }
}
