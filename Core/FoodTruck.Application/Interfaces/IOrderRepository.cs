using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderHeaderDto> CreateOrder(CartsDto cartsDto); 
        Task<StripeRequestDto> CreateStripe(StripeRequestDto stripeRequestDto);
        Task<OrderHeaderDto> ValidateStripe(int orderId);

    }
}
