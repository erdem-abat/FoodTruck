using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost("CartUpsert")]
        public async Task<IActionResult> CartUpsert(CartUpsertCommand cartUpsertCommand)
        {
            try
            {
                await _mediator.Send(cartUpsertCommand);
                _response.Result = "Cart operation has been completed.";
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
