using FoodTruck.Dto.OtpDtos;

namespace FoodTruck.Application.Interfaces;

public interface IOtpRepository
{
    OtpResponseDto CheckOtp(string email, string otp);
    ValidateResponseDto ValidateOtp(string email, string otpCode);
    Task<ValidateResponseDto> ResendOtpAsync(string email);
}
