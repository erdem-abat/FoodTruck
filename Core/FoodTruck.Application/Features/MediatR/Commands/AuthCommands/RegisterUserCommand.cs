using FoodTruck.Dto;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.AuthCommands
{
    public class RegisterUserCommand : IRequest<ResponseDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? OtpCode { get; set; }
    }
}
