using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Appuser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Roles { get; set; }
        public bool Isadmin { get; set; }
    }
}
