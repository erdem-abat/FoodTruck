using FoodTruck.Application.Features.MediatR.Queries.CartQueries;
using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Application.Interfaces;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class GetFoodTruckCartQueryHandler : IRequestHandler<GetFoodTruckCartQuery, GetFoodTruckCartQueryResult>
    {
        private readonly ICartRepository _cartRepository;

        public GetFoodTruckCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetFoodTruckCartQueryResult> Handle(GetFoodTruckCartQuery request, CancellationToken cancellationToken)
        {
            var values = new GetFoodTruckCartQueryResult();

            var cartResponse = _cartRepository.GetFoodTruckCart(request.UserId);

            if (cartResponse != null)
            {
                values.foodTruckCartsDto = cartResponse.Result;
                return values;
            }
            return values;
        }
    }
}
