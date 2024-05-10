using FoodTruck.Application.Features.MediateR.Queries.LocationQueries;
using FoodTruck.Application.Features.MediatR.Commands.RestaurantCommands;
using FoodTruck.Application.Features.MediatR.Commands.SeatCommands;
using FoodTruck.Application.Features.MediatR.Queries.ResturantQueries;
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
        public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand createRestaurantCommand)
        {
            try
            {
                var value = await _mediator.Send(createRestaurantCommand);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
        
        [HttpGet("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var values = await _mediator.Send(new GetRestaurantQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
        
        [HttpGet("GetRestaurantById")]
        public async Task<IActionResult> GetRestaurantById(int restaurantId)
        {
            try
            {
                var value = await _mediator.Send(new GetRestaurantByIdQuery(restaurantId));
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
        
        [HttpGet("GetLocations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var values = await _mediator.Send(new GetLocationQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateSeatPlan")]
        public async Task<IActionResult> CreateSeatPlan(CreateSeatPlanCommand createSeatPlanCommand)
        {
            try
            {
                var value = await _mediator.Send(createSeatPlanCommand);
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
        public async Task<IActionResult> AddFoodToRestaurant(CreateAddFoodToRestaurantCommand createAddFoodToRestaurantCommand)
        {
            try
            {
                var value = await _mediator.Send(createAddFoodToRestaurantCommand);
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
