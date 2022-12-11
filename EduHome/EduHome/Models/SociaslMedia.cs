using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class SociaslMedia
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Social Img is Required")]
        [StringLength(100)]
        public string SocialIcon { get; set; }
        [Required(ErrorMessage = "Slider Url is Required")]
        [StringLength(100)]
        public string SocialUrl { get; set; }
    }
}
