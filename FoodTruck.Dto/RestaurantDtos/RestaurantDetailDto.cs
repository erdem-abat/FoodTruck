﻿using System.Text.Json.Serialization;

namespace FoodTruck.Dto.RestaurantDtos
{
    public class RestaurantDetailDto
    {
        [JsonIgnore]
        public int? RestaurantDetailId { get; set; }
        [JsonIgnore]
        public int? RestaurantId { get; set; }
        public int LocationId { get; set; }
    }
}