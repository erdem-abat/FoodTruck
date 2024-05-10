using System.Text.Json.Serialization;
using FoodTruck.Dto.SeatDtos;
using FoodTruck.Dto.TableDtos;

namespace FoodTruck.Dto.RestaurantDtos;

public class RestaurantInfoDto
{
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string OpenTime { get; set; }
    public string CloseTime { get; set; }
    public bool IsAlcohol { get; set; }
    public int RestaurantDetailId { get; set; }
    public int LocationId { get; set; }
    public string locationName { get; set; }
    [JsonIgnore]
    public List<TableInfoDto>? TableInfoDtos { get; set; }
}