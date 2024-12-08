using System.ComponentModel.DataAnnotations;

namespace FoodTruck.WebUI.Models
{
    public class CartDto
    {
        [Required]
        public CartHeaderDto cartHeader { get; set; }
        [Required]
        public IEnumerable<CartDetailsDto>? cartDetails { get; set; }
    }
}
