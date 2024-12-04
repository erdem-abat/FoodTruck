using FoodTruck.Application.Features.MediateR.Results.FoodResults;
using MediatR;

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
