using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string StatusName { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
