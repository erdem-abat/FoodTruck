﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public IEnumerable<FoodMood> FoodMoods { get; set; }
        public IEnumerable<FoodTaste> FoodTastes { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<FoodTruck> Trucks { get; set; }
        public IEnumerable<FoodChef> Chefs { get; set; }
        public IEnumerable<FoodRestaurant> FoodRestaurants { get; set; }
    }
}