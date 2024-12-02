using FoodTruck.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodTruck.WebApi.Data
{
    public class UserIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserIdentityDbContext(DbContextOptions<UserIdentityDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasCharSet(null, DelegationModes.ApplyToDatabases);

            modelBuilder.Entity<ApplicationUser>(entity => { entity.ToTable(name: "Users"); });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany(e => e.LoginLogs)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.Email)
                      .HasPrincipalKey(e => e.Email);

                //entity.HasMany(e => e.FoodRates)
                //      .WithOne() 
                //      .HasForeignKey("UserId"); 

                //entity.HasMany(e => e.RestaurantUsers)
                //      .WithOne() 
                //      .HasForeignKey("UserId"); 
            });

            modelBuilder.Entity<RestaurantUser>()
               .HasKey(fi => new { fi.UserId, fi.RestaurantId });

            modelBuilder.Entity<RestaurantUser>()
              .HasOne(ur => ur.Restaurant)
              .WithMany(r => r.RestaurantUsers)
              .HasForeignKey(ur => ur.RestaurantId);

            modelBuilder.Entity<TruckReservation>()
                .HasOne(x => x.FromLocation)
                .WithMany(y => y.FromReservation)
                .HasForeignKey(z => z.FromLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            modelBuilder.Entity<TruckReservation>()
                .HasOne(x => x.ToLocation)
                .WithMany(y => y.ToReservation)
                .HasForeignKey(z => z.ToLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            modelBuilder.Entity<Food>()
              .HasMany(f => f.FoodRestaurants)
              .WithOne(fr => fr.Food)
              .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Restaurant>()
                .HasMany(f => f.FoodRestaurants)
                .WithOne(fr => fr.Restaurant)
                .HasForeignKey(fr => fr.RestaurantId);

            modelBuilder.Entity<FoodRestaurant>()
                .HasKey(fr => new { fr.FoodId, fr.RestaurantId });

            modelBuilder.Entity<Food>()
             .HasMany(f => f.FoodTastes)
             .WithOne(fr => fr.Food)
             .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Taste>()
                .HasMany(f => f.FoodTastes)
                .WithOne(fr => fr.Taste)
                .HasForeignKey(fr => fr.TasteId);

            modelBuilder.Entity<FoodTaste>()
                .HasKey(fr => new { fr.FoodId, fr.TasteId });

            modelBuilder.Entity<Food>()
            .HasMany(f => f.FoodTrucks)
            .WithOne(fr => fr.Food)
            .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Truck>()
                .HasMany(f => f.FoodTrucks)
                .WithOne(fr => fr.Truck)
                .HasForeignKey(fr => fr.TruckId);

            modelBuilder.Entity<FoodTruck.Domain.Entities.FoodTruck>()
                .HasKey(fr => new { fr.FoodId, fr.TruckId });

            modelBuilder.Entity<Food>()
              .HasMany(f => f.FoodCampaigns)
              .WithOne(fr => fr.Food)
              .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Campaign>()
                .HasMany(f => f.FoodCampaigns)
                .WithOne(fr => fr.Campaign)
                .HasForeignKey(fr => fr.CampaignId);

            modelBuilder.Entity<FoodCampaign>()
                .HasKey(fr => new { fr.FoodId, fr.CampaignId });

            modelBuilder.Entity<Food>()
               .HasMany(f => f.FoodMoods)
               .WithOne(fr => fr.Food)
               .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Mood>()
                .HasMany(f => f.Foods)
                .WithOne(fr => fr.Mood)
                .HasForeignKey(fr => fr.MoodId);

            modelBuilder.Entity<FoodMood>()
                .HasKey(fr => new { fr.FoodId, fr.MoodId });

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodIngredients)
                .WithOne(fr => fr.Food)
                .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Ingredient>()
                .HasMany(f => f.FoodIngredients)
                .WithOne(fr => fr.Ingredient)
                .HasForeignKey(fr => fr.IngredientId);

            modelBuilder.Entity<FoodIngredient>()
                .HasKey(fr => new { fr.FoodId, fr.IngredientId });

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

            modelBuilder.Entity<Food>()
                .HasMany(f => f.FoodChefs)
                .WithOne(fr => fr.Food)
                .HasForeignKey(fr => fr.FoodId);

            modelBuilder.Entity<Chef>()
                .HasMany(r => r.GoodAt)
                .WithOne(fr => fr.Chef)
                .HasForeignKey(fr => fr.ChefId);

            modelBuilder.Entity<FoodChef>()
                .HasKey(fr => new { fr.FoodId, fr.ChefId });
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
        public DbSet<FoodCampaign> FoodCampaigns { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RestaurantUser> RestaurantUsers { get; set; }
    }
}