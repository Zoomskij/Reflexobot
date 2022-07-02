using Reflexobot.Services.Inerfaces;
using Reflexobot.Services;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Repositories;
using Reflexobot.Data;
using Reflexobot.API;
using Reflexobot.Services.Interfaces;
using Reflexobot.API.Authorization;
using Reflexobot.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var env = builder.Environment;

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IUserService, UserService>();


// Add services to the container.
services.AddRazorPages();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient<IUpdateRepository, UpdateRepository>();
services.AddTransient<IReceiverService, ReceiverService>();

services.AddTransient<ICourseRepository, CourseRepository>();
services.AddTransient<ICourseService, CourseService>();

services.AddTransient<IStudentRepository, StudentRepository>();
services.AddTransient<IStudentService, StudentService>();

services.AddTransient<IAchievmentRepository, AchievmentRepository>();
services.AddTransient<IAchievmentService, AchievmentService>();

services.AddTransient<INoteRepository, NoteRepository>();
services.AddTransient<INoteService, NoteService>();

services.AddTransient<IGoalRepository, GoalRepository>();
services.AddTransient<IGoalService, GoalService>();

services.AddTransient<IScenarioRepository, ScenarioRepository>();
services.AddTransient<IScenarioService, ScenarioService>();

services.AddDbContext<ReflexobotContext>();
services.AddHostedService<PingBackgroundService>();
services.AddHostedService<NotifyBackgroundService>();
services.AddHostedService<TelegramBackgroundService>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.BuildServiceProvider();

var app = builder.Build();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

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