using FoodTruck.Application.Features.MediatR.Queries.AuthQueries;
using FoodTruck.Application.Features.MediatR.Results.AuthResults;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.AuthDtos;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodTruck.Application.Features.MediatR.Handlers.AuthHandlers
{
    public class GetLoginUserQueryHandler : IRequestHandler<GetLoginUserQuery, GetLoginUserQueryResult>
    {
        private readonly IAuthService _authService;

        public GetLoginUserQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GetLoginUserQueryResult> Handle(GetLoginUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetLoginUserQueryResult();
           
            var userResponse = _authService.Login(new LoginRequestDto
            {
                Username = request.Username,
                Password = request.Password
            });

            if (userResponse.Result.User == null)
            {
                values.IsExist = false;
            }
            else
            {
                values.IsExist = true;
                values.Username = userResponse.Result.User.Username;
                values.Roles = userResponse.Result.User.Roles;
                values.UserId = userResponse.Result.User.ID;
                values.AppUser = userResponse.Result.AppUser;
                values.Token = userResponse.Result.Token;
            }
            return values;
        }
    }
}
