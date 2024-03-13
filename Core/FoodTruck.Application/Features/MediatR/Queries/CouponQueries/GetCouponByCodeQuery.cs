using Amazon.Runtime.Internal;
using FoodTruck.Application.Features.MediatR.Results.CouponResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.CouponQueries
{
    public class GetCouponByCodeQuery : IRequest<GetCouponByCodeQueryResult>
    {
        public string Code { get; set; }

        public GetCouponByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
