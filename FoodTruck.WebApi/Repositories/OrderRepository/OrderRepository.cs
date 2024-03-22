using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.OrderDtos;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;

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

                orderHeaderDto.CreatedDate = DateTime.Now;
                orderHeaderDto.OrderStatus = orderStatus;
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
    }
}
