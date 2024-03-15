using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Dto.CartDtos;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Queries.CartQueries
{
    public class GetCartCouponApplyQuery : IRequest<GetCartCouponApplyQueryResult>
    {
        public CartsDto cartsDto { get; set; }

        public GetCartCouponApplyQuery(CartsDto cartsDto)
        {
            this.cartsDto = cartsDto;
        }
    }
}
