using Newtonsoft.Json;

namespace FoodTruck.Dto.AuthDtos;

public class UserTokenDto
{
    [JsonProperty("userId")]
    public string UserId { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }
    [JsonProperty("expires")]
    public DateTime Expires { get; set; }
}
