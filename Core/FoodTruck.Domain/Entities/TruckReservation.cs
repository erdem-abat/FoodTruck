namespace FoodTruck.Domain.Entities;

public class TruckReservation
{
    public int TruckReservationId { get; set; }
    public int TruckId { get; set; }
    public virtual Truck Truck { get; set; }
    public int? FromLocationId { get; set; }
    public int? ToLocationId { get; set; }
    public virtual Location FromLocation { get; set; }
    public virtual Location ToLocation { get; set; }
    public string Status { get; set; }
}