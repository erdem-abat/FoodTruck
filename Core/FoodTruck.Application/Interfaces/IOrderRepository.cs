using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Interfaces;

public interface IOrderRepository
{
    Task<OrderHeaderDto> CreateOrderAsync(CartDto cartDto); 
    Task<OrderHeaderDto> CreateOrderFoodTruckAsync(FoodTruckCartsDto foodTruckCartsDto); 
    Task<StripeRequestDto> CreateStripeAsync(StripeRequestDto stripeRequestDto);
    Task<OrderHeaderDto> ValidateStripeAsync(int orderId, int? truckId);

}
