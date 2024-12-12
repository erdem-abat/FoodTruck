using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Features.MediatR.Results.OrderResults
{
    public class CreateOrderCommandResult
    {
        public OrderHeaderDto OrderHeaderDto { get; set; }
    }
}
