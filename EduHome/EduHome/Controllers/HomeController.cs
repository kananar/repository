using EduHome.Dal;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.OrderBy(s => s.Order).ToList(),
                Setting = _context.Setting.FirstOrDefault(),
                noticeVioards = _context.NoticeVioards.ToList(),
                students=_context.Students.Include(s=>s.Position).ToList(),
                courses=_context.Courses.Take(3).ToList(),
                events=_context.Events.OrderByDescending(e=>e.StarDate).Take(4).ToList(),
                blogs=_context.Blogs.OrderByDescending(e=>e.DateTime).Take(3).ToList()

            };
            ViewBag.Settings = _context.Setting.FirstOrDefault();
            return View(homeVM);
        }


        public IActionResult IndexSearch()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> IndexSearch(string BlogSearch)
        {
            ViewData["GetAllDetails"] = BlogSearch;
            ViewBag.Settings = _context.Setting.FirstOrDefault();
         

            HomeVM homeVM = new HomeVM
            {
                Sliders = _context.Sliders.OrderBy(s => s.Order).ToList(),
                Setting = _context.Setting.FirstOrDefault(),
                noticeVioards = _context.NoticeVioards.ToList(),
                students = _context.Students.Include(s => s.Position).ToList(),
                courses = _context.Courses.Take(3).ToList(),
                events = _context.Events.OrderByDescending(e => e.StarDate).Take(4).ToList(),
                blogs = _context.Blogs.OrderByDescending(e => e.DateTime).Take(3).ToList()

            };
           
            var result = from x in _context.Comments.Include(b=>b.Blog).Include(c=>c.Event).Include(c=>c.Course) select x;
            if (!string.IsNullOrEmpty(BlogSearch))
            {
                HomeVM home = new HomeVM
                {
                   
                    courses =await _context.Courses.Where(c=>c.Name.Contains(BlogSearch)).AsNoTracking().ToListAsync(),
                    events = await _context.Events.Where(e=>e.Title.Contains(BlogSearch)).AsNoTracking().ToListAsync(),
                    blogs =await _context.Blogs.Where(e => e.Title.Contains(BlogSearch)).AsNoTracking().ToListAsync()
                };
               
                
                return View(home);
            }
            ViewBag.Result = await result.AsNoTracking().ToListAsync();
            
            return View(homeVM);

        }

    }
}

