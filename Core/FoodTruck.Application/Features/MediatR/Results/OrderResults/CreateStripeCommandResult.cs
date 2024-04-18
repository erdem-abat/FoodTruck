using FoodTruck.Dto.OrderDtos;

namespace FoodTruck.Application.Features.MediatR.Results.OrderResults
{
    public class CreateStripeCommandResult
    {
        public StripeRequestDto stripeRequestDto { get; set; }
    }
}
