using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.AuthDtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public ApplicationUser AppUser { get; set; }
        public string Token { get; set; }
    }
}
