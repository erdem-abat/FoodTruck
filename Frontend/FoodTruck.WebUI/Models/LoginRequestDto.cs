using System.ComponentModel.DataAnnotations;

namespace FoodTruck.WebUI.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
