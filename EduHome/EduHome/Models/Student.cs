using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FullName  is Required")]
        [StringLength(50)]
        public string FullName { get; set; }
        
        [StringLength(70)]
        public string Img { get; set; }
        [Required]
        public string Title { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> PositionIds { get; set; }
    }
}
