using FoodTruck.Application.Features.MediatR.Commands.ChefCommands;
using FoodTruck.Application.Features.MediatR.Queries.ChefQueries;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateChefAsync(ChefCreateCommand createChefCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(createChefCommand, cancellationToken);
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
        public async Task<IActionResult> GetChefAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetChefQuery(), cancellationToken);
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
