using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reflexobot.Entities;
using Reflexobot.Entities.Telegram;

namespace Reflexobot.Data
{
    public class ReflexobotContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public DbSet<UpdateEntity> Updates { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<StudentPersonIds> StudentPersonIds { get; set; }
        public DbSet<PersonPhraseEntity> PersonPhrases { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<NotifyEntity> Notifies { get; set; }
        public DbSet<StudentNotifyIds> StudentNotifyIds { get; set; }
        public DbSet<StudentCourseId> StudentCourseIds { get; set; }
        public DbSet<StudentLessonId> StudentLessonIds { get; set; }
        public DbSet<StudentTaskId> StudentTaskIds { get; set; }
        public DbSet<Achievment> Achievments { get; set; }
        public DbSet<StudentAchievment> StudentAchievments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: move to appsettings.json
            optionsBuilder.UseSqlServer("Data Source=SQL8002.site4now.net;Initial Catalog=db_a87f4e_reflexobot;User Id=db_a87f4e_reflexobot_admin;Password=reflexobot123");
        }

        public ReflexobotContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}