using Azure;
using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Features.MediatR.Commands.ChefCommands;
using FoodTruck.Application.Features.MediatR.Queries.ChefQueries;
using FoodTruck.Application.Interfaces;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;
        public ChefController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost("CreateChef")]
        public async Task<IActionResult> CreateChef(ChefCreateCommand createChefCommand)
        {
            try
            {
                await _mediator.Send(createChefCommand);
                _response.Result = "Chef successfully created.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("GetChef")]
        public async Task<IActionResult> GetChef()
        {
            try
            {
                var values = await _mediator.Send(new GetChefQuery());
                _response.Result = values;
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
