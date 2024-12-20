﻿using FoodTruck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Dto.OrderDtos
{
    public class OrderHeaderDto
    {
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public double OrderTotal { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public int? truckId { get; set; }

        public int OrderStatusId { get; set; }
        public IEnumerable<OrderDetailDto> OrderDetails { get; set; }
    }
}
