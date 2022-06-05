using Reflexobot.Services.Inerfaces;
using Reflexobot.Services;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Repositories;
using Reflexobot.Data;
using Reflexobot.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUpdateRepository, UpdateRepository>();
builder.Services.AddTransient<IReceiverService, ReceiverService>();
builder.Services.AddDbContext<ReflexobotContext>();
builder.Services.AddHostedService<TelegramBackgroundService>();
builder.Services.BuildServiceProvider();


var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

var app = builder.Build();

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