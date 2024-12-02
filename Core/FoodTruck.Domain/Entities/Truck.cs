namespace FoodTruck.Domain.Entities;

public class Truck
{
    public int TruckId { get; set; }
    public string TruckName { get; set; }
    public virtual ICollection<FoodTruck> FoodTrucks { get; set; }
    public virtual ICollection<Chef> Chefs { get; set; }
    public virtual ICollection<TruckReservation> TruckReservations { get; set; }
    public virtual ICollection<FoodTruckCartDetail> FoodTruckCartDetails { get; set; }
}
