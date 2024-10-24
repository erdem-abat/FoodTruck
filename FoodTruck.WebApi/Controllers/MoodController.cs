﻿using FoodTruck.Application.Features.MediatR.Commands.AuthCommands;
using FoodTruck.Application.Features.MediatR.Commands.MoodCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase
    {
        private readonly IMediator _mediator;

        protected readonly IConfiguration _configuration;

        public MoodController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("CreateMood")]
        public async Task<IActionResult> CreateMood(CreateMoodCommand createMoodCommand)
        {
            try
            {
                var value = await _mediator.Send(createMoodCommand);

                if (value)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}