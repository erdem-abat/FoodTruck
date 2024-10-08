using Microsoft.AspNetCore.Identity;

namespace FoodTruck.Domain.Entities;

public class ApplicationUser:IdentityUser
{
    public string Name { get; set; }
}