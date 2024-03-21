﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class Chef
    {
        public int ChefId { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public IEnumerable<Food> GoodAt { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
    }
}