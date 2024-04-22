using AutoMapper;
using Azure;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;
using FoodTruck.WebApi.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                foreach (var item in orderHeaderDto.OrderDetails)
                {
                    item.Price = cartsDto.CartDetails.First(x => x.FoodId == item.FoodId).Food.Price;
                }

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

        public async Task<OrderHeaderDto> CreateOrderFoodTruck(FoodTruckCartsDto foodTruckCartsDto)
        {
            try
            {
                OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(foodTruckCartsDto.CartHeader);

                var orderStatus = await _context.OrderStatuses.FirstAsync(x => x.StatusName == "Pending");
                var dateTime = DateTime.Now;

                orderHeaderDto.CreatedDate = dateTime.ToUniversalTime();
                orderHeaderDto.OrderStatusId = orderStatus.OrderStatusId;
                orderHeaderDto.truckId = foodTruckCartsDto.FoodTruckCartDetail.First().TruckId;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailDto>>(foodTruckCartsDto.FoodTruckCartDetail);
                foreach (var item in orderHeaderDto.OrderDetails)
                {
                    item.Price = foodTruckCartsDto.FoodTruckCartDetail.First(x => x.FoodId == item.FoodId).Food.Price;
                }
                if (orderHeaderDto.truckId != 0)
                {
                    foreach (var item in foodTruckCartsDto.FoodTruckCartDetail)
                    {
                        var truck = _context.FoodTrucks.First(x => x.FoodId == item.FoodId && x.TruckId == orderHeaderDto.truckId);
                        truck.Stock -= item.Count;
                        if (truck.Stock >= 0)
                        {
                            Order order = _context.Orders.Add(_mapper.Map<Order>(orderHeaderDto)).Entity;
                            await _context.SaveChangesAsync();

                            orderHeaderDto.OrderId = order.OrderId;
                        }
                        else
                        {
                            return orderHeaderDto;
                        }
                    }
                }

                else
                {
                    Order order = _context.Orders.Add(_mapper.Map<Order>(orderHeaderDto)).Entity;
                    await _context.SaveChangesAsync();

                    orderHeaderDto.OrderId = order.OrderId;
                }

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
                        Coupon = stripeRequestDto.orderHeaderDto.CouponCode
                    }
                };

                foreach (var item in stripeRequestDto.orderHeaderDto.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = _context.Foods.First(x => x.FoodId == item.FoodId).Name,
                            }
                        },
                        Quantity = item.Count
                    };

                    options.LineItems.Add(sessionLineItem);
                }

                if (stripeRequestDto.orderHeaderDto.Discount > 0)
                {
                    options.Discounts = discountsObj;
                }
                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;
                Order orderHeader = _context.Orders.First(x => x.OrderId == stripeRequestDto.orderHeaderDto.OrderId);
                orderHeader.StripeSessionId = session.Id;
                await _context.SaveChangesAsync();
                return stripeRequestDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<OrderHeaderDto> ValidateStripe(int orderId, int? truckId)
        {
            try
            {
                Order order = _context.Orders.First(x => x.OrderId == orderId);

                var service = new SessionService();
                Session session = service.Get(order.StripeSessionId);

                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                if (paymentIntent.Status == "succeeded")
                {
                    order.PaymentIntentId = paymentIntent.Id;
                    order.OrderStatus = _context.OrderStatuses.First(x => x.StatusName == "Approved");
                    await _context.SaveChangesAsync();

                    if (truckId != 0)
                    {
                        foreach (var item in order.OrderDetails)
                        {
                            var truck = _context.FoodTrucks.First(x => x.FoodId == item.FoodId && x.TruckId == truckId);
                            truck.Stock -= item.Count;
                            _context.FoodTrucks.Update(truck);
                            await _context.SaveChangesAsync();
                        }
                    }

                    //RewardsDto rewardsDto = new() //141. video topic ve sub oluşturma
                    //{
                    //    OrderId = order.OrderId,
                    //    RewardsActivity = Convert.ToInt32(order.OrderTotal),
                    //    UserId = order.UserId
                    //};

                    //string topicName = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
                    //_messageBus.SendMessage(rewardsDto, topicName);

                    var getCart = _context.CartHeaders.First(x => x.UserId == order.UserId);
                    _context.CartHeaders.Remove(getCart);
                    await _context.SaveChangesAsync();
                }

                return _mapper.Map<OrderHeaderDto>(order);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
