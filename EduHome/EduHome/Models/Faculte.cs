using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Faculte
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="FacultyName is Required")]
        [StringLength(50)]
        public string FacultyName { get; set; }
        [Required(ErrorMessage = "DEpartmentName is Required")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public List<FacultyTeacher> FacultyTeachers { get; set; }
    }
}
