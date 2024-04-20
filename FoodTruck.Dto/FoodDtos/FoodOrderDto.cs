﻿namespace FoodTruck.Dto.FoodDtos
{
    public class FoodOrderDto
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}