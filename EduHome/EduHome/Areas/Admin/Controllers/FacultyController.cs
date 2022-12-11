using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class FacultyController : Controller
    {
        private readonly AppDbContext _context;

        public FacultyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Faculte> facultes = _context.Facultes.ToList();
            return View(facultes);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Faculte faculte)
        {
            if (!ModelState.IsValid) return View(faculte);
          
          
         

            _context.Facultes.Add(faculte);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Faculte faculte = _context.Facultes.FirstOrDefault(c => c.Id == id);
            return View(faculte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Faculte faculte)
        {
            Faculte FaculteEdit = _context.Facultes.FirstOrDefault(ce => ce.Id == faculte.Id);
            if (!ModelState.IsValid) return View(FaculteEdit);
           
            if (faculte == null)
            {
                return NotFound();
            }


            FaculteEdit.DepartmentName = faculte.DepartmentName;
            FaculteEdit.FacultyName = faculte.FacultyName;
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            Faculte DeletFaculte = _context.Facultes.FirstOrDefault(dc => dc.Id == id);
            if (DeletFaculte == null) return Json(new { status = 404 });

            _context.Facultes.Remove(DeletFaculte);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
