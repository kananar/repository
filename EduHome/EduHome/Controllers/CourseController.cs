using EduHome.Dal;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Appuser> _userManager;

        public CourseController(AppDbContext context, UserManager<Appuser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 9);
            List<Course> courses = _context.Courses.Skip((page - 1) * 2).Take(9).ToList();
            ViewBag.User = _userManager.Users.ToList();
            return View(courses);
        }

        [HttpGet]

        public async Task<IActionResult> Index(string CourseSearch, int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 9);
            List<Course> courses = _context.Courses.Skip((page - 1) * 2).Take(9).ToList();
            ViewBag.User = _userManager.Users.ToList();
            if (!string.IsNullOrEmpty(CourseSearch))
            {
                ViewData["GetCourseDetails"] = CourseSearch;
                var result = from x in _context.Courses.Include(c => c.Category) select x;
                result = result.Where(x => x.Name.Contains(CourseSearch) || x.Category.Name.Contains(CourseSearch));
                return View(await result.AsNoTracking().ToListAsync());
            }

            return View(courses);

        }





        public IActionResult Deteails(int id)
        {

            HomeVM home = new HomeVM
            {
                course = _context.Courses.Include(c => c.CourseTags).Include(c=>c.Comments).ThenInclude(so=>so.Appuser).Include(c => c.Category).FirstOrDefault(c => c.Id == id),
                categories = _context.Categories.Include(c => c.Courses).ToList(),
                Tags = _context.Tags.Include(t => t.CourseTags).ThenInclude(t => t.Course).ToList(),
                courses = _context.Courses.Take(3).OrderByDescending(c => c.Starts).ToList(),
                appusers=_context.Users.ToList()
              
            };

            
            return View(home);
        }

        public IActionResult CategoryCourse(int Id)
        {
            List < Course > courses= _context.Courses.Where(c=>c.CategoryId==Id).ToList();
            return View(courses);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //return Content(user.Id);
            if (!ModelState.IsValid) return RedirectToAction("Deteails", "Course", new { id = comment.CourseId });
            if (!_context.Courses.Any(f => f.Id == comment.CourseId)) return NotFound();
            Comment Newcomment = new Comment
            {
                Message = comment.Message,
                CourseId = comment.CourseId,
                AppuserId = user.Id,
                Date=DateTime.Now
            };

            _context.Comments.Add(Newcomment);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Deteails", "Course", new { id = comment.CourseId });
            
        }


        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("deteails", "course");
            if (!_context.Comments.Any(c => c.AppuserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppuserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("deteails", "course", new { id = comment.CourseId });
        }
    }
}
