using FoodTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface ITruckRepository
    {
        Task<bool> CreateTruck(Truck truck, List<int> foodIds, List<int> chefIds);
    }
}
