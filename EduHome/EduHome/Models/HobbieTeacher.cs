using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class HobbieTeacher
    {
        public int Id { get; set; }
        public int HobbieId { get; set; }
        public int TeacherId { get; set; }
        public Hobbie Hobbie { get; set; }
        public Teacher Teacher { get; set; }
    }
}
