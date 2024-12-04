namespace FoodTruck.Application.Features.MediatR.Results.CouponResults
{
    public class GetCouponQueryResult
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int minAmount { get; set; }
    }
}
