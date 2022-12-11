using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Skill
    {
        public int Id { get; set; }
      
        [StringLength(30)]
        public string Name { get; set; }
        
       
        public int Point { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [NotMapped]
        public List<string> SkillNames { get; set; }
        [NotMapped]
        public List<int> SkillPoints { get; set; }
    }
}
