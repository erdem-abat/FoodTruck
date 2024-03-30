namespace FoodTruck.Dto.TruckReservationDtos
{
    public class TruckReservationRequestDto
    {
        public int TruckId { get; set; }
        public int? FromLocationId { get; set; }
        public int? ToLocationId { get; set; }
        public string Status { get; set; }
    }
}