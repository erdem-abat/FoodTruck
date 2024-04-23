using AutoMapper;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.CouponDtos;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.Dto.OrderDtos;
using FoodTruck.Dto.TruckDtos;
using FoodTruck.Dto.TruckReservationDtos;
using FoodTruck.WebApi.Controllers;

namespace FoodTruck.WebApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailDto>().ReverseMap();
                config.CreateMap<FoodTruckCartDetail, FoodTruckCartDetailDto>().ReverseMap();
                config.CreateMap<Order, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
                config.CreateMap<CartHeaderDto, OrderHeaderDto>().ReverseMap();
                config.CreateMap<CartDetailDto, OrderDetailDto>().ReverseMap();
                config.CreateMap<FoodTruckCartDetailDto, OrderDetailDto>().ReverseMap();
                config.CreateMap<TruckReservationRequestDto, TruckReservation>().ReverseMap();
                config.CreateMap<TruckReservationResponseDto, TruckReservation>().ReverseMap();
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
                config.CreateMap<Food, FoodDto>().ReverseMap();
                config.CreateMap<Truck, TruckDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}