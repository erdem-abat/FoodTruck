using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.OtpDtos;
using System.Net.Mail;
using System.Net;
using System.Text;
using FoodTruck.Dto.AuthDtos;
using FoodTruck.WebApi.Repositories.AuthRepository;

namespace FoodTruck.WebApi.Repositories.OtpRepository;

public class OtpRepository : IOtpRepository
{
    private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();
    private readonly IConfiguration _configuration;
    private readonly IRabbitMQAuthMessageSender _rabbitMQAuthMessageSender;

    public OtpRepository(IConfiguration configuration, IRabbitMQAuthMessageSender rabbitMQAuthMessageSender)
    {
        _configuration = configuration;
        _rabbitMQAuthMessageSender = rabbitMQAuthMessageSender;
    }

    public OtpResponseDto CheckOtp(string email, string? otp)
    {
        otpDictionary[email] = otp;

        try
        {
            SmtpClient client = new SmtpClient("smtp.ethereal.email", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("madilyn55@ethereal.email", _configuration["Email:Password"]);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("madilyn55@ethereal.email");
            mailMessage.To.Add(email);
            mailMessage.Subject = "subject";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>User Registered</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you For Registering account</p>" + otp);
            mailMessage.Body = mailBody.ToString();

            client.Send(mailMessage);

            return new OtpResponseDto
            {
                Response = "email sent.",
                success = true
            };
        }
        catch (SmtpException smtpEx)
        {
            Console.WriteLine("SMTP Exception: " + smtpEx.Message);
            Console.WriteLine("Status Code: " + smtpEx.StatusCode);
            if (smtpEx.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + smtpEx.InnerException.Message);
            }
            return new OtpResponseDto
            {
                Response = "email did not sent.",
                success = false
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }
            return new OtpResponseDto
            {
                Response = "email did not sent.",
                success = false
            };
        }
    }

    public ValidateResponseDto ValidateOtp(string email, string otpCode)
    {
        ValidateResponseDto res = new ValidateResponseDto();

        if (string.IsNullOrWhiteSpace(email) || !otpDictionary.ContainsKey(email))
        {
            res.Response = false;
            return res;
        }

        if (otpDictionary[email] == otpCode)
        {
            res.Response = true;
            otpDictionary.Remove(email);
            return res;
        }

        res.Response = false;
        return res;
    }

    public async Task<ValidateResponseDto> ResendOtpAsync(string email)
    {
        ValidateResponseDto res = new ValidateResponseDto();

        if (!string.IsNullOrWhiteSpace(email) || otpDictionary.ContainsKey(email))
        {
            res.Response = true;
            otpDictionary.Remove(email);
            CheckOtp(email, null);
            return res;
        }

        res.Response = false;
        return res;
    }
}