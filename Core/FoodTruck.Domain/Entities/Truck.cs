using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class Truck
    {
        public int TruckId { get; set; }
        public string TruckName { get; set; }
        public List<FoodTruck> Foods { get; set; }
        public List<Chef> Chefs { get; set; }
        public List<TruckReservation> TruckReservations { get; set; }
    }
}
