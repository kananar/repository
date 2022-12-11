using EduHome.Dal;
using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;

        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }

        public Setting GetSeting()
        {
            Setting setting = _context.Setting.FirstOrDefault();
            return setting;
        }
    }
}
