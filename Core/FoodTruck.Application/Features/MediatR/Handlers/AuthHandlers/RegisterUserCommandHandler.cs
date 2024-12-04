﻿using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto;
using FoodTruck.Dto.AuthDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, ResponseDto>
    {
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(new RegisterationRequestDto
            {
                Username = request.Username,
                Password = request.Password,
                otpCode = request.OtpCode
            });
        }
    }
}