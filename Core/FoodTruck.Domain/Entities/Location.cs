namespace FoodTruck.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<TruckReservation> FromReservation { get; set; }
        public List<TruckReservation> ToReservation { get; set; }
    }
}
