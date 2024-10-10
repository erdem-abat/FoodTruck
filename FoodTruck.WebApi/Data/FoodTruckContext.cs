using FoodTruck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

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

            modelBuilder.Entity<TruckReservation>()
                .HasOne(x => x.FromLocation)
                .WithMany(y => y.FromReservation)
                .HasForeignKey(z => z.FromLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<TruckReservation>()
                .HasOne(x => x.ToLocation)
                .WithMany(y => y.ToReservation)
                .HasForeignKey(z => z.ToLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FoodIngredient>()
            .HasKey(fi => new { fi.FoodId, fi.IngredientId });

            modelBuilder.Entity<Food>()
            .HasMany(f => f.Ingredients)
            .WithMany(i => i.Foods)
            .UsingEntity<FoodIngredient>(
                fi => fi.HasOne(fi => fi.Ingredient).WithMany().HasForeignKey(fi => fi.IngredientId),
                fi => fi.HasOne(fi => fi.Food).WithMany().HasForeignKey(fi => fi.FoodId)
            );

            modelBuilder.Entity<Food>()
                .HasMany(f => f.Campaigns)
                .WithMany(i => i.Foods)
                .UsingEntity<Dictionary<string, object>>(
                    "FoodCampaign",
                    j => j.HasOne<Campaign>().WithMany().HasForeignKey("CampaignId"),
                    j => j.HasOne<Food>().WithMany().HasForeignKey("FoodId"));

            modelBuilder.Entity<Food>()
               .HasMany(f => f.FoodRates)
               .WithOne(fr => fr.Food)
               .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Rate>()
                .HasMany(r => r.FoodRates)
                .WithOne(fr => fr.Rate)
                .HasForeignKey(fr => fr.RateId);

            modelBuilder.Entity<FoodRate>()
                .HasKey(fr => new { fr.FoodId, fr.RateId });
        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Taste> Tastes { get; set; }
        public DbSet<FoodTaste> FoodTaste { get; set; }
        public DbSet<FoodMood> FoodMood { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckReservation> TruckReservations { get; set; }
        public DbSet<FoodTruck.Domain.Entities.FoodTruck> FoodTrucks { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<FoodChef> FoodChefs { get; set; }
        public DbSet<FoodTruckCartDetail> FoodTruckCartDetails { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<RestaurantDetail> RestaurantDetails { get; set; }
        public DbSet<FoodRestaurant> FoodRestaurant { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Advertise> Advertises { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<FoodIngredient> FoodIngredient { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
