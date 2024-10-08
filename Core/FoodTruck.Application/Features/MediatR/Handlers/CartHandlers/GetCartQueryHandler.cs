﻿using FoodTruck.Application.Features.MediatR.Queries.CartQueries;
using FoodTruck.Application.Features.MediatR.Results.CartResults;
using FoodTruck.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Handlers.CartHandlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, GetCartQueryResult>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<GetCartQueryResult> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCartQueryResult();

            var cartResponse = _cartRepository.GetCart(request.UserId);

            if (cartResponse != null)
            {
                values.CartsDto = cartResponse.Result;
                return values;
            }
            return values;
        }
    }
}
