using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Features.MediatR.Results.OrderResults
{
    public class ValidateStripeCommandResult
    {
        public OrderHeaderDto orderHeaderDto { get; set; }
    }
}
