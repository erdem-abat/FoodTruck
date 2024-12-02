using Microsoft.AspNetCore.Identity;

namespace FoodTruck.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public virtual ICollection<LoginLog> LoginLogs { get; set; }
    public virtual ICollection<FoodRate> FoodRates { get; set; }
    public virtual ICollection<RestaurantUser> RestaurantUsers { get; set; }
}