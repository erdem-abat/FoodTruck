﻿namespace FoodTruck.Domain.Entities;

public class FoodChef
{
    public int FoodId { get; set; }
    public virtual Food Food { get; set; }
    public int ChefId { get; set; }
    public virtual Chef Chef { get; set; }
}