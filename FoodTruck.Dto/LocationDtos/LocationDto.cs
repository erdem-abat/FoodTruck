using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.LocationDtos
{
    public class LocationDto
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
