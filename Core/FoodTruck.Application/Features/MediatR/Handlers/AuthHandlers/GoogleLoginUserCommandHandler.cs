using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.AuthDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class GoogleLoginUserCommandHandler : IRequestHandler<GoogleLoginUserCommand, UserTokenDto>
    {
        private readonly IAuthService _authService;

        public GoogleLoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<UserTokenDto> Handle(GoogleLoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _authService.AuthenticateGoogleUserAsync(new GoogleRequestDto
            {
                IdToken = request.IdToken
            });
        }
    }
}