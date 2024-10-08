using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Domain.Entities;

public class Coupon
{
    public int CouponId { get; set; }
    [Required]
    public string CouponCode { get; set; }
    [Required]
    public double DiscountAmount { get; set; }
    public int minAmount { get; set; }
}
