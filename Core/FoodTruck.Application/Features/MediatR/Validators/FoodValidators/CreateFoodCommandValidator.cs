using FluentValidation;
using FoodTruck.Application.Features.MediateR.Commands.FoodCommands;
using FoodTruck.Domain.Entities;

namespace FoodTruck.Application.Features.MediatR.Validators.FoodValidators
{
    public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodCommandValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty()
             .WithMessage($"{nameof(Food.Name)} can not be empty!")
             .MaximumLength(20)
             .WithMessage($"{nameof(Food.Name)} must be less than 20 characters!");
        }
    }
}
