using FoodTruck.Domain.Entities;
using FoodTruck.Dto;
using FoodTruck.Dto.AuthDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string username, string roleName);
        Task<UserTokenDto> AuthenticateGoogleUserAsync(GoogleRequestDto request);
    }
}