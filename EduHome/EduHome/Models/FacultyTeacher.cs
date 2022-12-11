using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class FacultyTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int FaculteId { get; set; }
        public Faculte Faculte { get; set; }
        public Teacher Teacher { get; set; }
    }
}
