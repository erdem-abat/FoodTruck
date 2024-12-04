using FoodTruck.Application.Features.MediatR.Queries.CountryQueries;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetCountryQuery(), cancellationToken);
                _response.Result = values;
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
