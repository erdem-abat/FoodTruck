﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IRabbitMQAuthMessageSender
    {
        bool SendMessage(Object message, string queueName);
    }
}