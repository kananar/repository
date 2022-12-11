using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Slider
    {
        public int Id { get; set; }
       
        [StringLength(80)]
        public string SliderImg { get; set; }
        [Required(ErrorMessage = "Slider Title is Required")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Slider Description is Required")]
        [StringLength(250)]
        public string Descrtiption { get; set; }

        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
