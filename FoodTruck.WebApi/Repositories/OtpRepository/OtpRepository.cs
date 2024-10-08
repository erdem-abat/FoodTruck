using FoodTruck.Application.Interfaces;
using FoodTruck.Dto.OtpDtos;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace FoodTruck.WebApi.Repositories.OtpRepository;

public class OtpRepository : IOtpRepository
{
    private static Random r = new Random();
    private static Dictionary<string, string> otpDictionary = new Dictionary<string, string>();
    private readonly IConfiguration _configuration;

    public OtpRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public OtpResponseDto CheckOtp(string email)
    {
        var value = otpDictionary.FirstOrDefault(x => x.Key == email).Value;
        if (value == null)
        {
            string otp = GenerateOtp();
            otpDictionary[email] = otp;

            try
            {
                // Set up SMTP client
                SmtpClient client = new SmtpClient("smtp.ethereal.email", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("norma.rosenbaum94@ethereal.email", _configuration["Email:Password"]);

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("norma.rosenbaum94@ethereal.email");
                mailMessage.To.Add(email);
                mailMessage.Subject = "subject";
                mailMessage.IsBodyHtml = true;
                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("<h1>User Registered</h1>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<p>Thank you For Registering account</p>" + otp);
                mailMessage.Body = mailBody.ToString();

                // Send email
                client.Send(mailMessage);

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
                    Response = "email did not sent."
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
                    Response = "email did not sent."
                };
            }
            return new OtpResponseDto
            {
                Response = "Email sent!",
                success = true
            };
        }
        else
        {
            return new OtpResponseDto
            {
                Response = "Email already sent!",
                success = false
            };
        }

    }
    private string GenerateOtp()
    {
        return r.Next(100000, 999999).ToString();
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

    public async Task<ValidateResponseDto> ResendOtp(string email)
    {
        ValidateResponseDto res = new ValidateResponseDto();

        if (!string.IsNullOrWhiteSpace(email) || otpDictionary.ContainsKey(email))
        {
            res.Response = true;
            otpDictionary.Remove(email);
            CheckOtp(email);
            return res;
        }

        res.Response = false;
        return res;
    }
}