using FoodTruck.Dto.TruckReservationDtos;

namespace FoodTruck.Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<TruckReservationResponseDto> CreateReservation(TruckReservationRequestDto truckReservationRequestDto);
    }
}
