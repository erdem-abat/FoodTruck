﻿namespace FoodTruck.Domain.Entities;

public class Table
{
    public int TableId { get; set; }
    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
    public virtual IEnumerable<Seat> Seats { get; set; }
    public bool IsSmoking { get; set; }
}