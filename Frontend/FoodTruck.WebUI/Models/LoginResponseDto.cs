namespace FoodTruck.WebUI.Models
{
    public class LoginResponseDto
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
