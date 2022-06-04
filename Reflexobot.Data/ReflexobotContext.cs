using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reflexobot.Entities;

namespace Reflexobot.Data
{
    public class ReflexobotContext : DbContext
    {
        private readonly IConfiguration _configuration;
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder
        //        .UseNpgsql(_configuration.GetConnectionString("User ID=postgres;Password=1;Host=localhost;Port=5432;Database=reflexobt"));
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<UpdateEntity> Updates { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<UserPersonIds> UserPersonIds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=1;Host=localhost;Port=5432;Database=reflexobot");
        }
        //public ReflexobotContext(IConfiguration configuration) 
        //{
        //    Database.EnsureCreated();
        //    _configuration = configuration;
        //    Database.Migrate();
        //}
    }
}