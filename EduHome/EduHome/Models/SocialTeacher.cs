using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class SocialTeacher
    {
        public int Id { get; set; }
        [StringLength(80)]
        public  string Icon { get; set; }
        [StringLength(200)]
        public string Url { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [NotMapped]
        public List<string> Icons { get; set; }
        [NotMapped]
        public List<string> Urls { get; set; }
    }
}
