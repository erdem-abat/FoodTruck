using DnsClient;
using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.AuthDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var login = await _authService.Login(new LoginRequestDto
            {
                Username = request.Username,
                Password = request.Password
            });
        }
    }
}