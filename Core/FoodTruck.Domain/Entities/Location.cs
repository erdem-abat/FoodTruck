namespace FoodTruck.Domain.Entities;

public class Location
{
    public int LocationId { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public virtual IEnumerable<TruckReservation> FromReservation { get; set; }
    public virtual IEnumerable<TruckReservation> ToReservation { get; set; }
    public virtual IEnumerable<RestaurantDetail> Restaurants { get; set; }
}
