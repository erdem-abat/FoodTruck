using FoodTruck.Application.Features.MediatR.Queries.StatisticsQueries;
using FoodTruck.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpGet("GetTruckCount")]

        public async Task<IActionResult> GetTruckCountAsync()
        {
            var value = await _mediator.Send(new GetTruckCountQuery());
                
            _response.Result= value.TruckCount;
            _response.IsSuccess = true;

            return Ok(_response);
        }
    }
}
