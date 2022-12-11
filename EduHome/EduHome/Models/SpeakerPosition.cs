using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class SpeakerPosition
    {
        public int Id { get; set; }
        public int SpeakreId { get; set; }
        public int PositionId { get; set; }
        public Speaker Speaker { get; set; }
        public Position Position { get; set; }
    }
}
