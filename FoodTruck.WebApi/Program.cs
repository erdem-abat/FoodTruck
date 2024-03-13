using AutoMapper;
using FoodTruck.Application.Interfaces;
using FoodTruck.Application.Services;
using FoodTruck.Domain.Entities;
using FoodTruck.WebApi;
using FoodTruck.WebApi.Data;
using FoodTruck.WebApi.Extensions;
using FoodTruck.WebApi.Models;
using FoodTruck.WebApi.Repositories;
using FoodTruck.WebApi.Repositories.CartRepository;
using FoodTruck.WebApi.Repositories.CouponRepository;
using FoodTruck.WebApi.Repositories.FoodRepository;
using FoodTruck.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173");
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<FoodTruckContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<UserIdentityDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserIdentityDbContext>()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IFoodRepository), typeof(FoodRepository));
builder.Services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
builder.Services.AddScoped(typeof(ICouponRepository), typeof(CouponRepository));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddApplicationServices(builder.Configuration);

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddStackExchangeRedisCache(redisOptions=>
{
    string connection = builder.Configuration
    .GetConnectionString("Redis");

    redisOptions.Configuration = connection;
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.AddAppAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
