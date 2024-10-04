using FoodTruck.Domain.Entities;

namespace FoodTruck.Application.Features.MediatR.Results.AuthResults
{
    public class GetLoginUserQueryResult
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsExist { get; set; }
        public string Name { get; set; }
    }
}
