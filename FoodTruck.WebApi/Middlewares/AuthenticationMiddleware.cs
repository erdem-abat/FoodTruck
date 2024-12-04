using FoodTruck.Application.Exceptions;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;
using Newtonsoft.Json;
using ZstdSharp;

namespace FoodTruck.WebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserIdentityDbContext _dbContext)
        {
            if (context.Request.Path == "/api/Auth/login" && context.Request.Method == "POST")
            {

                context.Request.EnableBuffering();

                var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                dynamic bodyJson = JsonConvert.DeserializeObject(bodyAsText);
                string email = bodyJson?.username;
                var ipAddress = context.Connection.RemoteIpAddress?.ToString();  // Get IP address

                if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json"; // Set content type to JSON
                    var errorResponse = new
                    {
                        message = "Invalid email format."
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));

                    return; 
                }

                if(!_dbContext.Users.Any(x=>x.UserName==email))
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json"; // Set content type to JSON
                    var errorResponse = new
                    {
                        message = new NotFoundException($"User {email} not found!").Message
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));

                    return;
                }

                var loginLog = new LoginLog
                {
                    IpAddress = ipAddress,
                    Email = email,
                    LoginDate = DateTime.UtcNow
                };

                _dbContext.Attach<LoginLog>(loginLog);
                _dbContext.Entry<LoginLog>(loginLog).Property(x => x.Email).IsModified = true;
                _dbContext.Entry<LoginLog>(loginLog).Property(x => x.IpAddress).IsModified = true;
                _dbContext.Entry<LoginLog>(loginLog).Property(x => x.LoginDate).IsModified = true;

                await _dbContext.SaveChangesAsync();
            }

            await _next(context);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}