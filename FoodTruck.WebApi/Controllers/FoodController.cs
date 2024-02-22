using FoodTruck.Application.Features.MediatR.Queries.FoodQueries;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public FoodController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetFoods()
        {
            try
            {
                var values = await _mediator.Send(new GetFoodQuery());
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