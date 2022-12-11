using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Blog
    {
        public int Id { get; set; }
        
        [StringLength(90)]
        public string Img { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [StringLength(90)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [StringLength(600)]
        public string Description { get; set; }

        public DateTime DateTime { get; set; }
        public List<Comment> Comments { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
