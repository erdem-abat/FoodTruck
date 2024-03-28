using FoodTruck.Application.Features.MediatR.Commands.OrderCommands;
using FoodTruck.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.OrderHandlers
{
    public class CreateStripeCommandHandler : IRequestHandler<CreateStripeCommand>
    {
        private readonly IOrderRepository _repository;

        public CreateStripeCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateStripeCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateStripe(request.stripeRequestDto);
        }
    }
}
