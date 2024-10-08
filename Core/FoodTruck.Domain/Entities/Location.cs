namespace FoodTruck.Domain.Entities;

public class Location
{
    public int LocationId { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public IEnumerable<TruckReservation> FromReservation { get; set; }
    public IEnumerable<TruckReservation> ToReservation { get; set; }
    public IEnumerable<Restaurant> Restaurants { get; set; }
}
