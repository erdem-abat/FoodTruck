using FoodTruck.Application.Features.MediatR.Queries.CategoryQueires;
using FoodTruck.WebApi.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private ResponseDto _response;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
            _response = new ResponseDto();
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var values = await _mediator.Send(new GetCategoryQuery(), cancellationToken);
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
