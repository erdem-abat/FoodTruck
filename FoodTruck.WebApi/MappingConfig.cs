using AutoMapper;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;

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
            });
            return mappingConfig;
        }
    }
}