using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.EmailDtos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace FoodTruck.WebApi.Repositories.AuthRepository
{
    public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private static Random r = new Random();
        private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();
        public RabbitMQAuthMessageSender()
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
        }

        public bool SendMessage(object message, string queueName)
        {
            var value = otpDictionary.FirstOrDefault(x => x.Key == message.ToString()).Value;
            if (value == null)
            {
                string otp = GenerateOtp();
                otpDictionary[message.ToString()] = otp;

                OtpEmailDto otpEmailDto = new OtpEmailDto
                {
                    Otp = otp,
                    Email = message.ToString(),
                };


                if (ConnectionExists())
                {
                    using var channel = _connection.CreateModel();
                    channel.QueueDeclare(queueName, false, false, false, null);

                    var json = JsonConvert.SerializeObject(otpEmailDto);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: queueName, null, body: body);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return true;
        }

        private string GenerateOtp()
        {
            return r.Next(100000, 999999).ToString();
        }
    }
}
