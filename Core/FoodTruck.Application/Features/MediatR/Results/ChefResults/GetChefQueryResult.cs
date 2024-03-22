using FoodTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Results.ChefResults
{
    public class GetChefQueryResult
    {
        public int ChefId { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public int TruckId { get; set; }
    }
}
