using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Hobbie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "HobbieName is Required")]
        [StringLength(40)]
        public string Name { get; set; }
        public List<HobbieTeacher> HobbieTeachers { get; set; }
    }
}
