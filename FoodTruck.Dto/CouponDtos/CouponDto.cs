namespace FoodTruck.Dto.CouponDtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int minAmount { get; set; }
    }
}
