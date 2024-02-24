using MongoDB.Driver.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class FoodTaste
    {
        public int FoodTasteId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int TasteId { get; set; }
        public Taste Taste { get; set; }
    }
}
