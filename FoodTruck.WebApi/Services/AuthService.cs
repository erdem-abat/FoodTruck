﻿using Azure;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto;
using FoodTruck.Dto.AuthDtos;
using FoodTruck.Dto.OtpDtos;
using FoodTruck.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace FoodTruck.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserIdentityDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IOtpRepository _otprepository;
        private ResponseDto _response;
        protected readonly IConfiguration _configuration;

        public AuthService(UserIdentityDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator, IOtpRepository otprepository, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _otprepository = otprepository;
            _response = new ResponseDto();
            _configuration = configuration;
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

        public async Task<UserTokenDto> AuthenticateGoogleUserAsync(GoogleRequestDto request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[] { _configuration["ApiSettings:Google:ClientId"] }
            });

            return await GetOrCreateExternalLoginUser(GoogleRequestDto.PROVIDER, payload.Subject, payload.Email, payload.GivenName);
        }

        private async Task<UserTokenDto> GetOrCreateExternalLoginUser(string provider, string key, string email, string name)
        {
            var user = await _userManager.FindByLoginAsync(provider, key);
            var expires = DateTime.UtcNow.AddDays(7);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                return new UserTokenDto
                {
                    UserId = user.Id,
                    Token = token,
                    Expires = expires,
                    Email = email
                };
            }

            user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    Name = name,
                    Id = key,
                };
                await _userManager.CreateAsync(user);
                var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
                var result = await _userManager.AddLoginAsync(user, info);

                if (result.Succeeded)
                {
                    var userValue = _appDbContext.Users.FirstOrDefault(x => x.UserName == email);

                    if (!_roleManager.RoleExistsAsync("User").GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(userValue, "User");
                }

                if (!result.Succeeded)
                    throw new Exception(string.Join(",", result.Errors.Select(x => x.Description).ToList()));

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                return new UserTokenDto
                {
                    UserId = user.Id,
                    Token = token,
                    Expires = expires,
                    Email = email
                };
            }

            return new UserTokenDto();
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {
                return (new LoginResponseDto());
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                ID = user.Id,
                Roles = roles.ToList(),
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Username = user.UserName,
                Token = token
            };
        }

        public async Task<ResponseDto> Register(RegisterationRequestDto registerationRequestDto)
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
                    var emailSent = _otprepository.CheckOtp(registerationRequestDto.Email);

                    if (emailSent.success)
                    {
                        _response.Result = new RegisterationResponseDto()
                        {
                            Email = registerationRequestDto.Email,
                            Name = registerationRequestDto.Name,
                            otpCode = registerationRequestDto.otpCode,
                            Password = registerationRequestDto.Password,
                            PhoneNumber = registerationRequestDto.PhoneNumber,
                            Username = registerationRequestDto.Username
                        };

                        _response.Message = "Email sent!";
                        _response.IsSuccess = true;

                        return _response;
                    }

                    ValidateResponseDto res = _otprepository.ValidateOtp(registerationRequestDto.Email, registerationRequestDto.otpCode);

                    if (!res.Response)
                    {
                        _response.Message = "Otp is not valid!";
                        _response.IsSuccess = false;

                        return _response;
                    }
                    else
                    {
                        var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);

                        if (result.Succeeded)
                        {
                            userValue = _appDbContext.applicationUsers.FirstOrDefault(x => x.UserName == registerationRequestDto.Username);

                            if (!_roleManager.RoleExistsAsync("User").GetAwaiter().GetResult())
                            {
                                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
                            }
                            await _userManager.AddToRoleAsync(userValue, "User");
                        }
                        else
                        {
                            _response.Message = result.Errors.FirstOrDefault().Description;
                            _response.IsSuccess = false;

                            return _response;
                        }

                        var data = await Login(new LoginRequestDto()
                        {
                            Username = registerationRequestDto.Username,
                            Password = registerationRequestDto.Password
                        });


                        _response.Result = data;
                        _response.Message = "Registered!";
                        _response.IsSuccess = true;

                        return _response;
                    }
                }
                else
                {
                    _response.Message = "Account has already been created!";
                    _response.IsSuccess = true;

                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;

                return _response;
            }
        }
    }
}