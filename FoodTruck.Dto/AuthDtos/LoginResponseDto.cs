using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.AuthDtos
{
    public class LoginResponseDto
    {
        public string? ID { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public IEnumerable<string?> Roles { get; set; }
        public string? Token { get; set; }
    }
}
