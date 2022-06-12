using Reflexobot.Services.Inerfaces;
using Reflexobot.Services;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Repositories;
using Reflexobot.Data;
using Reflexobot.API;
using Reflexobot.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUpdateRepository, UpdateRepository>();
builder.Services.AddTransient<IReceiverService, ReceiverService>();

builder.Services.AddTransient<ICourseRepository, CourseRepository>();
builder.Services.AddTransient<ICourseService, CourseService>();

builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();

builder.Services.AddTransient<IAchievmentRepository, AchievmentRepository>();
builder.Services.AddTransient<IAchievmentService, AchievmentService>();

builder.Services.AddDbContext<ReflexobotContext>();
builder.Services.AddHostedService<PingBackgroundService>();
builder.Services.AddHostedService<NotifyBackgroundService>();
builder.Services.AddHostedService<TelegramBackgroundService>();
builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();