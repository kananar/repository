using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CommentMessage is Required")]
        [StringLength(300)]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int? EventId { get; set; }
        public int? BlogId { get; set; }
        public int? CourseId { get; set; }
        public string AppuserId { get; set; }
        public Appuser Appuser { get; set; }
        public Event Event { get; set; }
        public Blog Blog { get; set; }
        public Course Course { get; set; }
    }
}
