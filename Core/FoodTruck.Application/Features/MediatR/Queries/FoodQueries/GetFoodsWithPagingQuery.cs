using Amazon.Runtime.Internal;
using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Features.MediatR.Queries.FoodQueries
{
    public class GetFoodsWithPagingQuery : IRequest<List<GetFoodsWithPagingQueryResult>>
    {
        public GetFoodsWithPagingQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
