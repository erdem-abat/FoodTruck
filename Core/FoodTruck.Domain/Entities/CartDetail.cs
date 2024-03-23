using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Domain.Entities
{
    public class CartDetail
    {
        public int CartDetailId { get; set; }
        public int Count { get; set; }

        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public int FoodId { get; set; }
        [NotMapped]
        public Food Food { get; set; }
    }
}