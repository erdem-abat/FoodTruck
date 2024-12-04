using FoodTruck.Dto.AuthDtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Application.Features.MediatR.Commands.AuthCommands;

public class GoogleLoginUserCommand : IRequest<UserTokenDto>
{
    public const string PROVIDER = "google";

    [Required]
    public string IdToken { get; set; }
}
