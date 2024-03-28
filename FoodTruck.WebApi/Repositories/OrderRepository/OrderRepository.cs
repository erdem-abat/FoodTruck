using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace FoodTruck.WebApi.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private IMapper _mapper;
        private readonly FoodTruckContext _context;

        public OrderRepository(IMapper mapper, FoodTruckContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrderHeaderDto> CreateOrder(CartsDto cartsDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartsDto.CartHeader);

                var orderStatus = await _context.OrderStatuses.FirstAsync(x => x.StatusName == "Pending");
                var dateTime = DateTime.Now;

                orderHeaderDto.CreatedDate = dateTime.ToUniversalTime();
                orderHeaderDto.OrderStatusId = orderStatus.OrderStatusId;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailDto>>(cartsDto.CartDetails);

                Order order = _context.Orders.Add(_mapper.Map<Order>(orderHeaderDto)).Entity;
                await _context.SaveChangesAsync();

                orderHeaderDto.OrderId = order.OrderId;
                return orderHeaderDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StripeRequestDto> CreateStripe(StripeRequestDto stripeRequestDto)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment"
                };

                var discountsObj = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions
                    {
                        Coupon = stripeRequestDto.orderHeader.CouponCode
                    }
                };

                foreach (var item in stripeRequestDto.orderHeader.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Food.Name
                            }
                        },
                        Quantity = item.Quantity
                    };

                    options.LineItems.Add(sessionLineItem);
                }

                if (stripeRequestDto.orderHeader.Discount > 0)
                {
                    options.Discounts = discountsObj;
                }
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;
                Order orderHeader = _context.Orders.First(x => x.OrderId == stripeRequestDto.orderHeader.OrderId);
                orderHeader.StripeSessionId = session.Id;
                await _context.SaveChangesAsync();
                return stripeRequestDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
