﻿namespace FoodTruck.Domain.Entities
{
    public class FoodTaste
    {
        public int FoodTasteId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int TasteId { get; set; }
        public Taste Taste { get; set; }
    }
}
