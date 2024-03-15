using Azure;
using FoodTruck.Application.Features.MediatR.Commands.CartCommands;
using FoodTruck.Application.Features.MediatR.Commands.CouponCommands;
using FoodTruck.Application.Features.MediatR.Queries.CouponQueries;
using FoodTruck.Domain.Entities;
using FoodTruck.Dto.CartDtos;
using FoodTruck.Dto.CouponDtos;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ResponseDto> CreateCoupon(CouponCreateCommand couponCreateCommand)
        {
            try
            {
                await _mediator.Send(couponCreateCommand);
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
        public async Task<ResponseDto> GetCoupon()
        {
            try
            {
                var values = await _mediator.Send(new GetCouponQuery());
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
        public async Task<ResponseDto> GetCouponByCode([FromQuery] string code)
        {
            try
            {
                var values = await _mediator.Send(new GetCouponByCodeQuery(code));
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
        public async Task<ResponseDto> UpdateCoupon(CouponUpdateCommand couponUpdateCommand)
        {
            try
            {
                await _mediator.Send(couponUpdateCommand);
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
