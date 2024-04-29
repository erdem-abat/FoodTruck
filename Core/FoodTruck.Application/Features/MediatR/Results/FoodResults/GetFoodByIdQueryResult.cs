using FoodTruck.Dto.FoodDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediateR.Results.FoodResults
{
    public class GetFoodByIdQueryResult
    {
        public FoodWithAllDto foodWithAllDto { get; set; }
    }
}
