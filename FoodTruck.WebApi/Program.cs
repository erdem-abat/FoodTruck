using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Application.Services;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi;
using FoodTruck.WebApi.Data;
using FoodTruck.WebApi.Extensions;
using FoodTruck.WebApi.Handlers;
using FoodTruck.WebApi.Hubs;
using FoodTruck.WebApi.Message;
using FoodTruck.WebApi.Middlewares;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Repositories;
using FoodTruck.WebApi.Repositories.AuthRepository;
using FoodTruck.WebApi.Repositories.CartRepository;
using FoodTruck.WebApi.Repositories.CouponRepository;
using FoodTruck.WebApi.Repositories.FoodRepository;
using FoodTruck.WebApi.Repositories.OrderRepository;
using FoodTruck.WebApi.Repositories.OtpRepository;
using FoodTruck.WebApi.Repositories.ReservationRepository;
using FoodTruck.WebApi.Repositories.RestaurantRepository;
using FoodTruck.WebApi.Repositories.TruckRepository;
using FoodTruck.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("CorsPolicy", builder =>
//    {
//        builder.WithOrigins("exp://192.168.72.168:8081");
//        builder.AllowAnyHeader()
//        .AllowAnyMethod()
//        .SetIsOriginAllowed((host) => true)
//        .AllowCredentials();
//    });
//});

builder.Services.AddHttpClient();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<UserIdentityDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserIdentityDbContext>()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRabbitMQOtpService), typeof(RabbitMQOtpService));
builder.Services.AddScoped(typeof(IFoodRepository), typeof(FoodRepository));
builder.Services.AddSingleton(typeof(IOtpRepository), typeof(OtpRepository));
builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
builder.Services.AddSingleton(typeof(IRabbitMQAuthMessageSender), typeof(RabbitMQAuthMessageSender));
builder.Services.AddScoped(typeof(ICouponRepository), typeof(CouponRepository));
builder.Services.AddScoped(typeof(ITruckRepository), typeof(TruckRepository));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IReservationRepository), typeof(ReservationRepository));
builder.Services.AddScoped(typeof(IRestaurant), typeof(RestaurantRepository));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddHostedService<RabbitMQAuthConsumer>();
builder.Services.AddHostedService<GatewayRabbitMQAuthConsumer>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration
    .GetConnectionString("Redis");

    redisOptions.Configuration = connection;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: 'Bearer Generated-JWT-Token'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{ }
        }
    });

});

Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

builder.AddAppAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();
app.UseCors("CorsPolicy");
app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<AuthenticationMiddleware>();
app.MapHub<TruckHub>("/TruckHub");
app.Run();