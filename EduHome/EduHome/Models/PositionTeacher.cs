using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class PositionTeacher
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int TeacherId { get; set; }
        public Position Position { get; set; }
        public Teacher Teacher { get; set; }
    }
}
