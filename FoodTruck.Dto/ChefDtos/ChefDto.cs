using FoodTruck.Domain.Entities;
using FoodTruck.Dto.RestaurantDtos;
using FoodTruck.Dto.TruckDtos;

namespace FoodTruck.Dto.ChefDtos;

public class ChefDto
{
    public int ChefId { get; set; }
    public string Name { get; set; }
    public int Popularity { get; set; }
    public int TruckId { get; set; }

    public int RestaurantId { get; set; }
}
