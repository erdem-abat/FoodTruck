using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Features.MediatR.Queries.CartQueries;
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

        [HttpGet("GetCart/{userId}")]
        public async Task<IActionResult> GetCartAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetCartQuery(userId), cancellationToken);
                _response.Result = values.CartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpGet("GetFoodTruckCart")]
        public async Task<IActionResult> GetFoodTruckCartAsync([FromQuery] string userId, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetFoodTruckCartQuery(userId), cancellationToken);
                _response.Result = values.foodTruckCartsDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("FoodTruckCartUpsert")]
        public async Task<IActionResult> FoodTruckCartUpsertAsync(FoodTruckCartUpsertCommand foodTruckCartUpsertCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(foodTruckCartUpsertCommand, cancellationToken);
                _response.Result = "Cart operation has been completed.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("CartUpsert")]
        public async Task<IActionResult> CartUpsertAsync([FromBody] CartUpsertCommand cartUpsertCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(cartUpsertCommand, cancellationToken);
                _response.Result = "Cart operation has been completed.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCouponAsync(string UserId, string couponCode, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(new GetCartCouponApplyQuery(UserId, couponCode), cancellationToken);
                _response.Result = value.IsApply == true ? "Coupon applied!" : "Check coupon code!";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
        }

        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCartAsync(CartRemoveCommand cartRemoveCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(cartRemoveCommand, cancellationToken);
                _response.Result = value.IsDeleted == true ? "Cart removed!" : "Check the cart!";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
