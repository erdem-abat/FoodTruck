namespace FoodTruck.Application.Interfaces;

public interface IRabbitMQAuthMessageSender
{
    bool SendMessage(Object message, string queueName);
}
