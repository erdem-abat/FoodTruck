using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Features.MediatR.Results.OrderResults
{
    public class CreateOrderFoodTruckCommandResult
    {
        public OrderHeaderDto orderHeaderDto { get; set; }
    }
}
