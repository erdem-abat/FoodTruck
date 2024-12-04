using FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;
using FoodTruck.Application.Features.MediatR.Commands.SeatCommands;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost("CreateRestaurant")]
        public async Task<IActionResult> CreateRestaurantAsync(CreateRestaurantCommand createRestaurantCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createRestaurantCommand, cancellationToken);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut("ApproveRestaurant")]
        public async Task<IActionResult> ApproveRestaurantAsync(ApproveRestaurantCommand createRestaurantCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createRestaurantCommand, cancellationToken);

                if (value)
                {
                    _response.Message = "Restaurant has been approved.";

                    return StatusCode(StatusCodes.Status201Created, _response);
                }

                _response.Message = "Something went wrong!";

                return StatusCode(StatusCodes.Status304NotModified, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateSeatPlan")]
        public async Task<IActionResult> CreateSeatPlanAsync(CreateSeatPlanCommand createSeatPlanCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createSeatPlanCommand, cancellationToken);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
        [HttpPost("AddFoodToRestaurant")]
        public async Task<IActionResult> AddFoodToRestaurantAsync(CreateAddFoodToRestaurantCommand createAddFoodToRestaurantCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createAddFoodToRestaurantCommand, cancellationToken);
                _response.Result = value;
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
