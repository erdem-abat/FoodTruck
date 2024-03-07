using Amazon.Runtime.Internal;
using FoodTruck.Application.Features.MediatR.Results.CartResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.CartQueries
{
    public class GetCartQuery : IRequest<GetCartQueryResult>
    {
        public string UserId { get; set; }

        public GetCartQuery(string userId)
        {
            UserId = userId;
        }
    }
}
