using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
