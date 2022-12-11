using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class ItemsVM
    {
        public Teacher teacher { get; set; }
        public SocialTeacher socialTeacher { get; set; }
        public Skill Skill { get; set; }
        //public List< Skill> Skills { get; set; }
    }
}
