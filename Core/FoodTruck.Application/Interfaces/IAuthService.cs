using FoodTruck.Dto;
using FoodTruck.Dto.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string username, string roleName);
    }
}
