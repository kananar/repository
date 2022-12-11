using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public Setting Setting { get; set; }
        public List<NoticeVioard> noticeVioards { get; set; }
        public List<Student> students { get; set; }
        public List<Teacher> teachers { get; set; }
        public Teacher teacher { get; set; }
        public List<Course> courses { get; set; }
        public Course course { get; set; }
        public Event eevent { get; set; }
        public List<Event> events { get; set; }
        public List<Category> categories { get; set; }
        public List<Blog> blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Position> positions { get; set; }
        public List<Hobbie> hobbies { get; set; }
        public List<Faculte> facultes { get; set; }
        public List<Appuser> appusers { get; set; }
        public Contact contact { get; set; }
        
    }
}
