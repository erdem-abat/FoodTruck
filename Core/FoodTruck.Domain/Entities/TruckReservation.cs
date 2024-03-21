using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class TruckReservation
    {
        public int TruckReservationId { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public Location FromLocation { get; set; }
        public Location ToLocation { get; set; }
        public string Status { get; set; }
    }
}