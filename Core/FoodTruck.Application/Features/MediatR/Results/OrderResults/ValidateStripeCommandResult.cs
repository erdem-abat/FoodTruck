using FoodTruck.Dto.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Results.OrderResults
{
    public class ValidateStripeCommandResult
    {
        public OrderHeaderDto orderHeaderDto { get; set; }
    }
}
