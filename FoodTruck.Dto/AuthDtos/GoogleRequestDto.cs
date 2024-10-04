using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Dto.AuthDtos;

public class GoogleRequestDto
{
    public const string PROVIDER = "google";

    //[JsonProperty("idToken")]
    [Required]
    public string IdToken { get; set; }
}