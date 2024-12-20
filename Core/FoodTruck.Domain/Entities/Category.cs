﻿using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<Food> Foods { get; set; }
    }
}
