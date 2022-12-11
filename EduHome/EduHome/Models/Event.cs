using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Event
    {
        public int Id { get; set; }
        
        [StringLength(90)]
        public string Img { get; set; }
        [Required(ErrorMessage = "Title is Required")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        
        public string Description { get; set; }
        [Required(ErrorMessage = "Venu is Required")]
        [StringLength(100)]
        public string Venue { get; set; }
        [Required(ErrorMessage = "StartTime is Required")]
       
        public DateTime StarDate { get; set; }
        [Required(ErrorMessage = "FinisDate is Required")]
        public DateTime FinisDate { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<Comment> Comments { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> SpeakersIds { get; set; }
    }
}
