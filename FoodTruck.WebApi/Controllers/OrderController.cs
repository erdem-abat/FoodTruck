using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            try
            {
                await _mediator.Send(createOrderCommand);
                _response.Result = "Order successfully created.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPost("CreateStripeSession")]
        public async Task<IActionResult> CreateStripeSession(CreateStripeCommand createStripeCommand)
        {
            try
            {
                await _mediator.Send(createStripeCommand);
                _response.Result = "Stripe successfully created.";
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
