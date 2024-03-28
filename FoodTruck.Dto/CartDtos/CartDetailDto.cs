using FoodTruck.Domain.Entities;
using FoodTruck.Dto.FoodDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Dto.CartDtos
{
    public class CartDetailDto
    {
        public int CartDetailId { get; set; }
        public int Count { get; set; }

        public int CartHeaderId { get; set; }
        public int FoodId { get; set; }
        public FoodDto? Food { get; set; }
    }
}
