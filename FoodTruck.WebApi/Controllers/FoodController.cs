using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediatR.Commands.FoodCommands;
using FoodTruck.Application.Features.MediatR.Queries.ChefQueries;
using FoodTruck.Application.Features.MediatR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediatR.Queries.IngredientQueries;
using FoodTruck.Application.Features.MediatR.Queries.MoodQueries;
using FoodTruck.Application.Features.MediatR.Queries.TasteQueries;
using FoodTruck.Dto.FoodDtos;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Admin")]
        [HttpGet("GetFoods")]
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

        [HttpGet("GetFoodById")]
        public async Task<IActionResult> GetFoodById(int foodId)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodByIdQuery(foodId));
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("GetFoodsWithPaging")]
        public async Task<IActionResult> GetFoodsWithPaging(int page, int pageSize)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodsWithPagingQuery(page,pageSize));
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("GetFoodsMainPage")]
        public async Task<IActionResult> GetFoodsMainPage()
        {
            try
            {
                var values = await _mediator.Send(new GetFoodMainPageQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpGet("GetMood")]
        public async Task<IActionResult> GetMood()
        {
            try
            {
                var values = await _mediator.Send(new GetMoodQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("GetTaste")]
        public async Task<IActionResult> GetTaste()
        {
            try
            {
                var values = await _mediator.Send(new GetTasteQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("GetIngredient")]
        public async Task<IActionResult> GetIngredient()
        {
            try
            {
                var values = await _mediator.Send(new GetIngredientQuery());
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("GetFoodsByFilter/getFoodsByFilterParameters")]
        public async Task<IActionResult> GetFoodsByFilter([FromQuery]GetFoodsByFilterParameters getFoodsByFilterParameters)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodsByFilterQuery(getFoodsByFilterParameters));
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateFood")]
        public async Task<IActionResult> CreateFood(CreateFoodCommand createFoodCommand)
        {
            try
            {
                await _mediator.Send(createFoodCommand);
                _response.Result = "Food successfully created.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateFoodWithExcel")]
        public async Task<IActionResult> CreateFoodWithExcel(CreateFoodWithExcelCommand createFoodWithExcelCommand)
        {
            try
            {
                _response.Result = await _mediator.Send(createFoodWithExcelCommand);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFood(UpdateFoodCommand updateFoodCommand)
        {
            try
            {
                await _mediator.Send(updateFoodCommand);
                _response.Result = "Food successfully updated.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFood(int id)
        {
            try
            {
                await _mediator.Send(new RemoveFoodCommand(id));
                _response.Result = "Food successfully deleted.";
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