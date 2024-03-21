using FoodTruck.Application.Features.MediatR.Commands.ChefCommands;
using FoodTruck.Application.Features.MediatR.Commands.TruckCommands;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;
        public TruckController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost("CreateTruck")]
        public async Task<IActionResult> CreateTruck(CreateTruckCommand createTruckCommand)
        {
            try
            {
                var value = await _mediator.Send(createTruckCommand);
                _response.Result = value.IsCreated == true ? "Truck successfully created." : "Check the values!";
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
