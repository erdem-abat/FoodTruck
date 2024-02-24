using FoodTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetFoodsWithCategory();
        Task<List<Food>> GetFoodsWithAll();
    }
}
