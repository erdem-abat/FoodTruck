using FoodTruck.Application.Features.MediatR.Commands.CouponCommands;
using FoodTruck.Application.Features.MediatR.Queries.CouponQueries;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public CouponController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<ResponseDto> CreateCouponAsync(CouponCreateCommand couponCreateCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(couponCreateCommand, cancellationToken);
                _response.Result = "Coupon has been created.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetCoupon")]
        public async Task<ResponseDto> GetCouponAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetCouponQuery(), cancellationToken);
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetCouponByCode")]
        public async Task<ResponseDto> GetCouponByCodeAsync([FromQuery] string code, CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetCouponByCodeQuery(code), cancellationToken);
                _response.Result = values;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public async Task<ResponseDto> UpdateCouponAsync(CouponUpdateCommand couponUpdateCommand, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(couponUpdateCommand, cancellationToken);
                _response.Result = "Coupon successfully updated.";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


    }
}
