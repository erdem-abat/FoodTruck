using System.ComponentModel.DataAnnotations;

namespace FoodTruck.WebUI.Models
{
    public class RegisterationRequestDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? OtpCode { get; set; }
    }
}
