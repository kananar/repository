using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class EventSpeaker
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int SpekaerId { get; set; }
        public Event Event { get; set; }
        public Speaker Spekaer { get; set; }
    }
}
