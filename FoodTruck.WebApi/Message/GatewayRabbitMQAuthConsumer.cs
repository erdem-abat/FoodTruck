using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.EmailDtos;

namespace FoodTruck.WebApi.Message
{
    public class GatewayRabbitMQAuthConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private readonly IOtpRepository _otpRepository;

        public GatewayRabbitMQAuthConsumer(IConfiguration configuration, IOtpRepository otpRepository)
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
            _channel.QueueDeclare(_configuration.GetValue<string>("GatewayQueue:GatewayQueueName"), false, false, false, null);

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

                HandleMessage(res.Email, res.Otp);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.GetValue<string>("GatewayQueue:GatewayQueueName"), false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string email, string otp)
        {
            _otpRepository.CheckOtp(email, otp);
        }
    }
}