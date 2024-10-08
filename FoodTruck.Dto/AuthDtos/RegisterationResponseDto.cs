namespace FoodTruck.Dto.AuthDtos;

public class RegisterationResponseDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? otpCode { get; set; }
}
