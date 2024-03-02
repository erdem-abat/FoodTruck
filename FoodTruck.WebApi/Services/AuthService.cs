using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.AuthDtos;
using FoodTruck.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace FoodTruck.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserIdentityDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserIdentityDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string username, string roleName)
        {
            var user = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {
                return (new LoginResponseDto()
                {
                    User = null,
                    AppUser=null,
                    Token = ""
                });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDTO = new()
            {
                Username = user.UserName,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList()
            };

            return new LoginResponseDto
            {
                User = userDTO,
                AppUser = user,
                Token = token
            };
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerationRequestDto.Username,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                Name = registerationRequestDto.Name,
                PhoneNumber = registerationRequestDto.PhoneNumber
            };

            try
            {
                var userValue = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName == registerationRequestDto.Username);

                if (userValue == null)
                {
                    var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);

                    if (result.Succeeded)
                    {
                        userValue = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName == registerationRequestDto.Username);

                        if (!_roleManager.RoleExistsAsync(registerationRequestDto.Role).GetAwaiter().GetResult())
                        {
                            _roleManager.CreateAsync(new IdentityRole(registerationRequestDto.Role)).GetAwaiter().GetResult();
                        }
                        await _userManager.AddToRoleAsync(userValue, registerationRequestDto.Role);
                    }
                    else
                    {
                        return result.Errors.FirstOrDefault().Description;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return "Error Encountered";
        }
    }
}