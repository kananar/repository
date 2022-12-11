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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Teacher> teachers = _context.Teachers.Include(t => t.SocialTeachers).Include(t=>t.PositionTeachers).ThenInclude(pt=>pt.Position).ToList();
            return View(teachers);
        }
        public IActionResult Details(int Id)
        {
            HomeVM home = new HomeVM
            {
             teacher =_context.Teachers.Include(t=>t.SocialTeachers).Include(t=>t.Skills).FirstOrDefault(t=>t.Id==Id),
             positions =_context.Positions.Where(p=>p.PositionTeachers.Any(p=>p.TeacherId==Id)).ToList(),
             hobbies=_context.Hobbies.Where(h=>h.HobbieTeachers.Any(h=>h.TeacherId==Id)).ToList(),
             facultes=_context.Facultes.Where(f=>f.FacultyTeachers.Any(ft=>ft.TeacherId==Id)).ToList()

             
             
            };
            return View(home);
        }
    }
}
