using Microsoft.Extensions.Configuration;
using NetCoreRedisEntegrationBusiness.Interfaces;
using NetCoreRedisEntegrationBusiness.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

var app = builder.Build();

//builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(new ConfigurationOptions
//{
//    EndPoints = { $"{configuration.GetConnectionString("Redis")}" },
//    AbortOnConnectFail = false,
//    Ssl = true,
//}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
