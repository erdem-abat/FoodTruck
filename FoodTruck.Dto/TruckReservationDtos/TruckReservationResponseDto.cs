using FoodTruck.Domain.Entities;

namespace FoodTruck.Dto.TruckReservationDtos
{
    public class TruckReservationResponseDto
    {
        public int TruckReservationId { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public Location FromLocation { get; set; }
        public Location ToLocation { get; set; }
        public string Status { get; set; }
    }
}