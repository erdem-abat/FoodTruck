using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Features.MediateR.Queries.FoodQueries;
using FoodTruck.Application.Features.MediatR.Commands.FoodCommands;
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
        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetFoods")]
        public async Task<IActionResult> GetFoodsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodQuery(), cancellationToken);
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
        public async Task<IActionResult> GetFoodByIdAsync(int foodId, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodByIdQuery(foodId), cancellationToken);
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
        public async Task<IActionResult> GetFoodsWithPagingAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodsWithPagingQuery(page, pageSize), cancellationToken);
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
        public async Task<IActionResult> GetFoodsMainPageAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodMainPageQuery(), cancellationToken);
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
        public async Task<IActionResult> GetMoodAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetMoodQuery(), cancellationToken);
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
        public async Task<IActionResult> GetTasteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetTasteQuery(), cancellationToken);
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
        public async Task<IActionResult> GetIngredientAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetIngredientQuery(), cancellationToken);
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
        public async Task<IActionResult> GetFoodsByFilterAsync([FromQuery] GetFoodsByFilterParameters getFoodsByFilterParameters, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodsByFilterQuery(getFoodsByFilterParameters), cancellationToken);
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
        public async Task<IActionResult> CreateFoodAsync(CreateFoodCommand createFoodCommand, CancellationToken cancellationToken)
        {
        //    try
        //    {
        //        await _mediator.Send(createFoodCommand, cancellationToken);
        //        _response.Result = "Food successfully created.";
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Result = ex;
        //    }
        //    return Ok(_response);

            await _mediator.Send(createFoodCommand, cancellationToken);
            _response.Result = "Food successfully created.";
            return Ok(_response);
        }

        [HttpPost("CreateFoodWithExcel")]
        public async Task<IActionResult> CreateFoodWithExcelAsync(CreateFoodWithExcelCommand createFoodWithExcelCommand, CancellationToken cancellationToken)
        {
            try
            {
                _response.Result = await _mediator.Send(createFoodWithExcelCommand, cancellationToken);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut("updateFood/{id}")]
        public async Task<IActionResult> UpdateFoodAsync(int id, [FromBody] UpdateFoodCommand updateFoodCommand, CancellationToken cancellationToken)
        {
            try
            {
                updateFoodCommand.FoodId = id;
                await _mediator.Send(updateFoodCommand, cancellationToken);
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
        public async Task<IActionResult> RemoveFoodAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(new RemoveFoodCommand(id), cancellationToken);
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