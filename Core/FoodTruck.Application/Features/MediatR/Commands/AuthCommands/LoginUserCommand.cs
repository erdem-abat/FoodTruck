using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.AuthCommands
{
    public class LoginUserCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
