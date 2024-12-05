using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Features.MediatR.Queries.AuthQueries;
using FoodTruck.WebApi.Models.Dto;
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
        protected readonly IConfiguration _configuration;

        public AuthController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _response = new ResponseDto();
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserCommand registerUserCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(registerUserCommand, cancellationToken);

                if (value.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK, value);
                }
                else
                {
                    return Ok(value.Message);
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
        public async Task<IActionResult> LoginAsync(GetLoginUserQuery getLoginUserQuery, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(getLoginUserQuery, cancellationToken);

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
        public async Task<IActionResult> GoogleLoginAsync(GoogleLoginUserCommand googleLoginUserCommand, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));

                return Ok(await _mediator.Send(googleLoginUserCommand, cancellationToken));
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRoleAsync(AssignRoleUserCommand assignRoleUserCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(assignRoleUserCommand, cancellationToken);
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