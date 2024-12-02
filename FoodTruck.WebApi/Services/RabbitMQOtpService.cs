using FoodTruck.Application.Interfaces;
using FoodTruck.Dto;

namespace FoodTruck.WebApi.Services
{
    public class RabbitMQOtpService : IRabbitMQOtpService
    {
        private readonly IRabbitMQAuthMessageSender _rabbitMQAuthMessageSender;
        private readonly IConfiguration _configuration;

        public RabbitMQOtpService(IRabbitMQAuthMessageSender rabbitMQAuthMessageSender, IConfiguration configuration)
        {
            _rabbitMQAuthMessageSender = rabbitMQAuthMessageSender;
            _configuration = configuration;
        }

        public ResponseDto EmailSent(string email)
        {
            ResponseDto res = new ResponseDto();

            var data = _rabbitMQAuthMessageSender.SendMessage(email, _configuration.GetValue<string>("QueueName:RegisterUserOtpQueue"));

            if (data)
            {
                res.IsSuccess = true;
                return res;
            }
            else
            {
                res.IsSuccess = false;
                res.Message = "Email already sent!";
                return res;
            }
        }
    }
}