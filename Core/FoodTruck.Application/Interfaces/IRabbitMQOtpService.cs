﻿using FoodTruck.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IRabbitMQOtpService
    {
        ResponseDto EmailSent(string email);
    }
}
