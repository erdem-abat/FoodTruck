using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.EmailDtos;

namespace FoodTruck.WebApi.Message
{
    public class RabbitMQAuthConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private readonly IOtpRepository _otpRepository;

        public RabbitMQAuthConsumer(IConfiguration configuration, IOtpRepository otpRepository)
        {
            _configuration = configuration;
            _otpRepository = otpRepository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("QueueName:RegisterUserOtpQueue"), false, false, false, null);

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                OtpEmailDto res = new OtpEmailDto();
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                res = JsonConvert.DeserializeObject<OtpEmailDto>(content);

                HandleMessageAsync(res.Email, res.Otp);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.GetValue<string>("QueueName:RegisterUserOtpQueue"), false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(string email, string otp)
        {
            _otpRepository.CheckOtp(email, otp);
        }
    }
}