using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrderAsync(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createOrderCommand, cancellationToken);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }


        [HttpPost("CreateOrderFoodTruck")]
        public async Task<IActionResult> CreateOrderFoodTruckAsync(CreateOrderFoodTruckCommand createOrderFoodTruckCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createOrderFoodTruckCommand, cancellationToken);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateStripeSession")]
        public async Task<IActionResult> CreateStripeSessionAsync(CreateStripeCommand createStripeCommand, CancellationToken cancellationToken)
        {
            try
            {
                var value = await _mediator.Send(createStripeCommand, cancellationToken);
                _response.Result = value;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("ValidateStripeSession")]
        public async Task<IActionResult> ValidateStripeSessionAsync(ValidateStripeCommand validateStripeCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(validateStripeCommand, cancellationToken);
                _response.Result = "Stripe successfull.";
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
