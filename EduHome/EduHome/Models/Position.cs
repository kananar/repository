using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "PositionName is Required")]
        [StringLength(50)]
        public string Name { get; set; }
        public List<PositionTeacher> PositionTeachers { get; set; }
        public List<Student> Students { get; set; }
        public List<SpeakerPosition> SpeakerPositions { get; set; }
    }
}
