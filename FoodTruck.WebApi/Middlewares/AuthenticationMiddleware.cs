using FoodTruck.Domain.Entities;
using FoodTruck.WebApi.Data;
using Newtonsoft.Json;

namespace FoodTruck.WebApi.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, FoodTruckContext _dbContext)
        {
            if (context.Request.Path == "/api/Auth/login" && context.Request.Method == "POST")
            {

                context.Request.EnableBuffering();

                var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                dynamic bodyJson = JsonConvert.DeserializeObject(bodyAsText);
                string email = bodyJson?.username;
                var ipAddress = context.Connection.RemoteIpAddress?.ToString();  // Get IP address

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
    }
}