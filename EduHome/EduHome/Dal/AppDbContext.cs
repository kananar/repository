using EduHome.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Dal
{
    public class AppDbContext : IdentityDbContext<Appuser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTag> Coursetags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Faculte> Facultes { get; set; }
        public DbSet<FacultyTeacher> FacultyTeachers { get; set; }
        public DbSet<Hobbie> Hobbies { get; set; }
        public DbSet<HobbieTeacher> HobbieTeachers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionTeacher> PositionTeachers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        
        public DbSet<SocialTeacher> socialTeachers { get; set; }
        public DbSet<SociamMediaSetting> SociamMediaSettings { get; set; }
        public DbSet<SociaslMedia> SociaslMedias { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerPosition> SpeakerPositions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<NoticeVioard> NoticeVioards { get; set; }
        public DbSet<Contact> Contacts { get; set; }



    }

   
}

