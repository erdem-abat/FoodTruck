using FoodTruck.Application.Features.MediatR.Results.OrderResults;
using FoodTruck.Dto.OrderDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Commands.OrderCommands
{
    public class CreateStripeCommand : IRequest<CreateStripeCommandResult>
    {
        public string? StripeSessionUrl { get; set; }
        public string? StripeSessionId { get; set; }
        public string ApprovedUrl { get; set; }
        public string CancelUrl { get; set; }
        public OrderHeaderDto orderHeaderDto { get; set; }
    }
}