﻿namespace FoodTruck.Domain.Entities;

public class Chef
{
    public int ChefId { get; set; }
    public string Name { get; set; }
    public int Popularity { get; set; }
    public IEnumerable<FoodChef> GoodAt { get; set; }
    public int TruckId { get; set; }
    public virtual Truck Truck { get; set; }
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
