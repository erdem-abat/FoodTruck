using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTruck.Domain.Entities;

public class CartDetail
{
    public int CartDetailId { get; set; }
    public int Count { get; set; }

    public int CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public  CartHeader CartHeader { get; set; }
    public int FoodId { get; set; }
    [NotMapped]
    public Food Food { get; set; }
}