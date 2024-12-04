using FoodTruck.Dto;

namespace FoodTruck.Application.Interfaces;

public interface IRabbitMQOtpService
{
    ResponseDto EmailSent(string email);
}
