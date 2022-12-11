using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class SociamMediaSetting
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string SocialIcon { get; set; }
      
        [StringLength(100)]
        public string SocialUrl { get; set; }
        public int SettingId { get; set; }
        public Setting Setting { get; set; }
    }
}
