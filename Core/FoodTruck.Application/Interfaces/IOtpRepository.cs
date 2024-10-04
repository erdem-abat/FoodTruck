using FoodTruck.Dto.OtpDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruck.Application.Interfaces
{
    public interface IOtpRepository
    {
        OtpResponseDto CheckOtp(string email);
        ValidateResponseDto ValidateOtp(string email, string otpCode);
        Task<ValidateResponseDto> ResendOtp(string email);
    }
}
