using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderHeaderDto> CreateOrder(CartsDto cartsDto); 
        Task<OrderHeaderDto> CreateOrderFoodTruck(FoodTruckCartsDto foodTruckCartsDto); 
        Task<StripeRequestDto> CreateStripe(StripeRequestDto stripeRequestDto);
        Task<OrderHeaderDto> ValidateStripe(int orderId, int? truckId);

    }
}
