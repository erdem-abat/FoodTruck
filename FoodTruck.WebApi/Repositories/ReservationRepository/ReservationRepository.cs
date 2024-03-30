using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.TruckReservationDtos;
using FoodTruck.WebApi.Data;

namespace FoodTruck.WebApi.Repositories.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private IMapper _mapper;
        private readonly FoodTruckContext _context;

        public ReservationRepository(IMapper mapper, FoodTruckContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<TruckReservationResponseDto> CreateReservation(TruckReservationRequestDto truckReservationRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
