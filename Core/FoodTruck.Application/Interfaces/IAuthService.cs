using FoodTruck.Dto;
using FoodTruck.Dto.AuthDtos;

namespace FoodTruck.Application.Interfaces;

public interface IAuthService
{
    Task<ResponseDto> RegisterAsync(RegisterationRequestDto registerationRequestDto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    Task<bool> AssignRoleAsync(string username, string roleName);
    Task<UserTokenDto> AuthenticateGoogleUserAsync(GoogleRequestDto request);
}