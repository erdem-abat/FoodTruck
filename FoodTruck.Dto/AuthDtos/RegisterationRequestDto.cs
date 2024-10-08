namespace FoodTruck.Dto.AuthDtos
{
    public class RegisterationRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? otpCode { get; set; }
    }
}