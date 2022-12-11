using EduHome.Areas.Extensions;
using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 5);
            List<Course> courses = _context.Courses.Skip((page - 1) * 5).Take(5).ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid) return View(course);
            
            course.CourseTags = new List<CourseTag>();
            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
            if (course.TagIds == null)
            {
                ModelState.AddModelError("TagIds", "Image is required");
                return View();
            }

            else
            {
                if (!course.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Image file is required");
                    return View();
                }

                if (!course.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "Image file max size 2mb");
                    return View();
                }
                course.Img = course.ImageFile.SaveImage(_env.WebRootPath, "assets/img/course");
            }
            foreach (int id in course.TagIds)
            {
                CourseTag courseTag = new CourseTag
                {
                    Course = course,
                    TagId = id
                };
                course.CourseTags.Add(courseTag);
            }
          


            _context.Courses.Add(course);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            if (!ModelState.IsValid) return View();
            Course course = _context.Courses.Include(c => c.Category).Include(c => c.CourseTags).ThenInclude(c=>c.Tag).FirstOrDefault(c => c.Id == Id);
            if (course == null)
            {
                return NotFound();
            }

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Category = _context.Categories.ToList();
            Course ExsistCourse = await _context.Courses.Include(c => c.CourseTags).ThenInclude(c => c.Tag).Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == course.Id);

           
            if (!ModelState.IsValid) return View(ExsistCourse);
            
            if (ExsistCourse == null)
            {
                return NotFound();
            }
            if (course.TagIds==null)
            {
                ModelState.AddModelError("", "tag name is required");
                return View();
            }
        
            if (course.ImageFile !=null)
            {
                if (!course.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "image file required");
                    return View();
                }
                if (!course.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "image max  size 2mb required");
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", ExsistCourse.Img);
                ExsistCourse.Img = course.ImageFile.SaveImage(_env.WebRootPath, "assets/img/course");
            }


            List<CourseTag> removableCategories = ExsistCourse.CourseTags.Where(fc => !course.TagIds.Contains(fc.Id)).ToList();

            ExsistCourse.CourseTags.RemoveAll(fc => removableCategories.Any(rc => fc.Id == rc.Id));


            foreach (var tag in course.TagIds)
            {
                CourseTag courseTag = ExsistCourse.CourseTags.FirstOrDefault(fc => fc.TagId == tag);
                if (courseTag == null)
                {
                    CourseTag CourseTag1 = new CourseTag
                    {
                        TagId = tag,
                        CourseId = ExsistCourse.Id
                    };
                    ExsistCourse.CourseTags.Add(CourseTag1);
                }
            }


            ExsistCourse.Name = course.Name;
            ExsistCourse.CategoryId = course.CategoryId;
            ExsistCourse.Language = course.Language;
            ExsistCourse.AboutCourse = course.AboutCourse;
            ExsistCourse.Assesment = course.Assesment;
            ExsistCourse.ClassDuration = course.ClassDuration;
            ExsistCourse.Description = course.Description;
            ExsistCourse.Price = course.Price;
            ExsistCourse.Serftication = course.Serftication;
            ExsistCourse.Duration = course.Duration;
            ExsistCourse.Starts = course.Starts;
            ExsistCourse.Student = course.Student;
            ExsistCourse.SkillLevel = course.SkillLevel;
            ExsistCourse.HowToApply = course.HowToApply;
            ExsistCourse.Assesment = course.Assesment;
            await _context.SaveChangesAsync();

            


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return Json(new { status = 404 });
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", course.Img);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return Json(new { status = 200 });
            
        }

    }
}
