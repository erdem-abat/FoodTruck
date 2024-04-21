using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Features.MediatR.Queries.CartQueries;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
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

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart([FromQuery] string userId)
        {
            try
            {
                var values = await _mediator.Send(new GetCartQuery(userId));
                _response.Result = values.CartsDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return Ok(_response);
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

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon(string UserId, string couponCode)
        {
            try
            {
                var value = await _mediator.Send(new GetCartCouponApplyQuery(UserId, couponCode));
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
        public async Task<ResponseDto> RemoveCart(CartRemoveCommand cartRemoveCommand)
        {
            try
            {
                var value = await _mediator.Send(cartRemoveCommand);
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
