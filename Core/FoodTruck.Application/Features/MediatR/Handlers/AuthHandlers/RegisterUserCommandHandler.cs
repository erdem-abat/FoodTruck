using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.AuthDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand>
    {
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var errorMesage = await _authService.Register(new RegisterationRequestDto
            {
                Username = request.Username,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
                Role = "USER"
            });
        }
    }
}