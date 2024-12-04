using FoodTruck.Application.Features.MediatR.Commands.TruckCommands;
using FoodTruck.Application.Features.MediatR.Queries.TruckQuerires;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
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
        public async Task<IActionResult> CreateTruckAsync(CreateTruckCommand createTruckCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createTruckCommand, cancellationToken);
                _response.Result = value.IsCreated == true ? "Truck successfully created." : "Check the values!";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("GetFoodTruck")]
        public async Task<IActionResult> GetFoodTruckAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodTruckQuery(), cancellationToken);
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
