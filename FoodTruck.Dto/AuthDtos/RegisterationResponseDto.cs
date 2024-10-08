namespace FoodTruck.Dto.AuthDtos;

public class RegisterationResponseDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? otpCode { get; set; }
}
