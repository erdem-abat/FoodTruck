using AutoMapper;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.CouponDtos;
using FoodTruck.Dto.FoodDtos;
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
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
                config.CreateMap<Food, FoodDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}