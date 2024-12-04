namespace FoodTruck.Dto.AuthDtos
{
    public class LoginResponseDto
    {
        public string? ID { get; set; }
        public string? Username { get; set; }
        public IEnumerable<string?> Roles { get; set; }
        public string? Token { get; set; }
    }
}
