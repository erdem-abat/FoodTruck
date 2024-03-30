using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodTruck.Application.Features.MediatR.Commands.FoodCommands
{
    public class CreateFoodWithExcelCommand : IRequest<CreateFoodWithExcelCommandResult>
    {
        public IFormFile formFile { get; set; }
    }
}
