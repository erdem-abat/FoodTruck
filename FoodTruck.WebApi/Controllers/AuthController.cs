using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Features.MediatR.Queries.AuthQueries;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.AuthDtos;
using FoodTruck.WebApi.Models.Dto;
using FoodTruck.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;
        protected readonly IConfiguration _configuration;

        public AuthController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _response = new ResponseDto();
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUserCommand)
        {
            try
            {
                var value = await _mediator.Send(registerUserCommand);

                if (value.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK, value);
                }
                else
                {
                    return BadRequest(value.Message);
                }
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

        [HttpPost("googleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginUserCommand googleLoginUserCommand)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));

                return Ok(await _mediator.Send(googleLoginUserCommand));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
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