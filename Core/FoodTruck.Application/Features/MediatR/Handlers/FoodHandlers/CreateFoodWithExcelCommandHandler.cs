using FoodTruck.Application.Features.MediatR.Commands.FoodCommands;
using FoodTruck.Application.Features.MediatR.Results.FoodResults;
using FoodTruck.Application.Interfaces;
using MediatR;
using System.Data;

namespace FoodTruck.Application.Features.MediatR.Handlers.FoodHandlers
{
    public class CreateFoodWithExcelCommandHandler : IRequestHandler<CreateFoodWithExcelCommand, CreateFoodWithExcelCommandResult>
    {
        private readonly IFoodRepository _repository;

        public CreateFoodWithExcelCommandHandler(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateFoodWithExcelCommandResult> Handle(CreateFoodWithExcelCommand request, CancellationToken cancellationToken)
        {
            string path = await _repository.DocumentUpload(request.formFile);
            DataTable dt = await _repository.FoodDataTable(path);
            var data = await _repository.ImportFood(dt);

            return new CreateFoodWithExcelCommandResult
            {
                message = data.ToString()
            };
        }
    }
}