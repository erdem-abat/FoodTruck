using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Features.MediatR.Queries.AuthQueries;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.AuthDtos;
using FoodTruck.WebApi.Models.Dto;
using FoodTruck.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IMediator mediator, IJwtTokenGenerator jwtTokenGenerator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            try
            {
                await _mediator.Send(registerUserCommand);
                _response.Result = "Register successfully.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetLoginUserQuery getLoginUserQuery)
        {
            try
            {
                var values = await _mediator.Send(getLoginUserQuery);

                if (values.IsExist)
                {
                    _response.Result = values;
                }
                else
                {
                    _response.Message = "Username or password invalid.";
                    _response.IsSuccess = false;
                }
               
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleUserCommand assignRoleUserCommand)
        {
            try
            {
                await _mediator.Send(assignRoleUserCommand);
                _response.Result = "Role successfully assigned.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
    }
}