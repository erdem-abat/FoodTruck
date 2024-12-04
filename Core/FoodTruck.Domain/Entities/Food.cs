using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class Food
{
    [Key]
    public int FoodId { get; set; }
    [Required]
    public string Name { get; set; }
    [Range(1, 1000)]
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageLocalPath { get; set; }
    [NotMapped]
    public IFormFile? Image { get; set; }
    public double price { get; set; }

    public int CountryId { get; set; }
    public virtual Country Country { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual IEnumerable<CartDetail> CartDetails { get; set; }
    public virtual IEnumerable<FoodMood> FoodMoods { get; set; }
    public virtual IEnumerable<FoodTaste> FoodTastes { get; set; }
    public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    public virtual IEnumerable<FoodTruck> FoodTrucks { get; set; }
    public virtual IEnumerable<FoodChef> FoodChefs { get; set; }
    public virtual IEnumerable<FoodRestaurant> FoodRestaurants { get; set; }
    public virtual IEnumerable<FoodIngredient> FoodIngredients { get; set; }
    public virtual IEnumerable<FoodRate> FoodRates { get; set; }
    public virtual IEnumerable<FoodCampaign> FoodCampaigns { get; set; }
    public virtual IEnumerable<Advertise> Advertises { get; set; }
    public virtual IEnumerable<FoodTruckCartDetail> FoodTruckCartDetails { get; set; }
}