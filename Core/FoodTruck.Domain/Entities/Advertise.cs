using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class Advertise
    {
        public int AdvertiseId { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
        public int EndDate { get; set; }
    }
}
