using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class AssignRoleUserCommandHandler : IRequestHandler<AssignRoleUserCommand>
    {
        private readonly IAuthService _authService;

        public AssignRoleUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task Handle(AssignRoleUserCommand request, CancellationToken cancellationToken)
        {
            var assignRoleStatus = await _authService.AssignRole(request.Username, request.Role.ToUpper());
        }


    }
}
