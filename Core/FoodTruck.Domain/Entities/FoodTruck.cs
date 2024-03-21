namespace FoodTruck.Domain.Entities
{
    public class FoodTruck
    {
        public int FoodTruckId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
    }
}
