using EduHome.Areas.Extensions;
using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentController( AppDbContext context, IWebHostEnvironment env) 
        {
           _context = context;
            _env = env;
        }
        public IActionResult Index( )
        {
            List<Student> students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            ViewBag.Position = _context.Positions.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            ViewBag.Position = _context.Positions.ToList();
            if (!ModelState.IsValid) return View();

            if (student.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
          

            if (student.ImageFile != null)
            {
                if (!student.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Image file  required");
                    return View();
                }
                if (!student.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "Image file max size must be 2mb ");
                    return View();
                }

                student.Img = student.ImageFile.SaveImage(_env.WebRootPath, "assets/img/testimonial");
            }

           
            
           



            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("index", "student");
        }

        public IActionResult Edit(int id)
        {
            Student student = _context.Students.FirstOrDefault(e => e.Id == id);
            ViewBag.Position = _context.Positions.ToList();

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {

            Student exsistStudent = _context.Students.FirstOrDefault(e => e.Id == student.Id);
            ViewBag.Position = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(exsistStudent);

            if (exsistStudent == null)
            {
                return NotFound();
            }
            if (student.ImageFile != null)
            {
                if (!student.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "please choose image file");
                    return View(exsistStudent);
                }
                if (!student.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "IMAGE SIZE MUST BE MAX 2MB");
                    return View(exsistStudent);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", exsistStudent.Img);
                exsistStudent.Img = student.ImageFile.SaveImage(_env.WebRootPath, "assets/img/testimonial");
            }

            exsistStudent.PositionId = student.PositionId;
            exsistStudent.FullName = student.FullName;
            exsistStudent.Title = student.Title;
            _context.SaveChanges();



            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            Student student = _context.Students.FirstOrDefault(e => e.Id == id);
            if (student == null) return Json(new { status = 404 });
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", student.Img);
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
