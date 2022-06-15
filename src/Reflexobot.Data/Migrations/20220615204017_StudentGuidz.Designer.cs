﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reflexobot.Data;

#nullable disable

namespace Reflexobot.Data.Migrations
{
    [DbContext(typeof(ReflexobotContext))]
    [Migration("20220615204017_StudentGuidz")]
    partial class StudentGuidz
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Reflexobot.Entities.Achievment", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Achievments");
                });

            modelBuilder.Entity("Reflexobot.Entities.CourseEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Reflexobot.Entities.GroupEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("CourseGuid");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Reflexobot.Entities.LessonEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("CourseGuid");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Reflexobot.Entities.NotifyEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Notifies");
                });

            modelBuilder.Entity("Reflexobot.Entities.Person", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Reflexobot.Entities.PersonPhraseEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Phrase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("PersonPhrases");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentAchievment", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AchievmentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StudentEntityGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("AchievmentGuid");

                    b.HasIndex("StudentEntityGuid");

                    b.ToTable("StudentAchievments");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentCourseId", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("StudentCourseIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("GroupGuid");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentLessonId", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<Guid>("LessonGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentCourseIdGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("StudentCourseIdGuid");

                    b.ToTable("StudentLessonIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentNotifyIds", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("NotifyGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("StudentNotifyIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentPersonIds", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.ToTable("StudentPersonIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentTaskId", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentLessonIdGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaskGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Guid");

                    b.HasIndex("StudentLessonIdGuid");

                    b.ToTable("StudentTaskIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.TaskEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LessonGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("LessonGuid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Reflexobot.Entities.Telegram.ChatEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Reflexobot.Entities.Telegram.MessageEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("ChatGuid");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Reflexobot.Entities.Telegram.UpdateEntity", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<Guid>("MessageGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Guid");

                    b.HasIndex("MessageGuid");

                    b.ToTable("Updates");
                });

            modelBuilder.Entity("Reflexobot.Entities.GroupEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.CourseEntity", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Reflexobot.Entities.LessonEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.CourseEntity", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentAchievment", b =>
                {
                    b.HasOne("Reflexobot.Entities.Achievment", "Achievment")
                        .WithMany()
                        .HasForeignKey("AchievmentGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Reflexobot.Entities.StudentEntity", null)
                        .WithMany("StudentAchievments")
                        .HasForeignKey("StudentEntityGuid");

                    b.Navigation("Achievment");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.GroupEntity", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupGuid");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentLessonId", b =>
                {
                    b.HasOne("Reflexobot.Entities.StudentCourseId", null)
                        .WithMany("StudentLessonIds")
                        .HasForeignKey("StudentCourseIdGuid");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentTaskId", b =>
                {
                    b.HasOne("Reflexobot.Entities.StudentLessonId", null)
                        .WithMany("StudentTaskIds")
                        .HasForeignKey("StudentLessonIdGuid");
                });

            modelBuilder.Entity("Reflexobot.Entities.TaskEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.LessonEntity", "Lesson")
                        .WithMany("Tasks")
                        .HasForeignKey("LessonGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Reflexobot.Entities.Telegram.MessageEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.Telegram.ChatEntity", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Reflexobot.Entities.Telegram.UpdateEntity", b =>
                {
                    b.HasOne("Reflexobot.Entities.Telegram.MessageEntity", "Message")
                        .WithMany()
                        .HasForeignKey("MessageGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Reflexobot.Entities.CourseEntity", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Reflexobot.Entities.GroupEntity", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Reflexobot.Entities.LessonEntity", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentCourseId", b =>
                {
                    b.Navigation("StudentLessonIds");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentEntity", b =>
                {
                    b.Navigation("StudentAchievments");
                });

            modelBuilder.Entity("Reflexobot.Entities.StudentLessonId", b =>
                {
                    b.Navigation("StudentTaskIds");
                });
#pragma warning restore 612, 618
        }
    }
}
