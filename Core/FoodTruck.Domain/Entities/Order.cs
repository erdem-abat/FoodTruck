namespace FoodTruck.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public double OrderTotal { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
