using EduHome.Dal;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Setting=_context.Setting.FirstOrDefault(),
                noticeVioards=_context.NoticeVioards.ToList(),
                students = _context.Students.Include(s => s.Position).ToList(),
                teachers=_context.Teachers.Include(t=>t.SocialTeachers).Include(t=>t.PositionTeachers).ThenInclude(py=>py.Position).ToList()

            };
            return View(homeVM);
        }
    }
}
