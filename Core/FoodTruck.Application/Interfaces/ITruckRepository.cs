using FoodTruck.Domain.Entities;
using FoodTruck.Dto.TruckDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface ITruckRepository
    {
        Task<bool> CreateTruck(Truck truck, List<int[][]> foodIdsWithStocks, List<int> chefIds);
        Task<List<FoodTruckDto>> GetFoodTrucks();
    }
}
