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
    public class HobbieController : Controller
    {
        private readonly AppDbContext _context;
        public HobbieController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Hobbie> hobbies = _context.Hobbies.ToList();
            return View(hobbies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hobbie hobbie)
        {
            if (!ModelState.IsValid) return View();
            Hobbie SameName = _context.Hobbies.FirstOrDefault(sm => sm.Name.ToLower().Trim() == hobbie.Name.ToString().Trim());
            if (hobbie.Name == null)
            {
                ModelState.AddModelError("Name", "hobbie Name Is required");
            }
            
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {hobbie.Name} alrady taken");
                return View();
            }

            _context.Hobbies.Add(hobbie);
            _context.SaveChanges();

            return RedirectToAction("index", "hobbie");
        }

        public IActionResult Edit(int id)
        {
            Hobbie hobbie = _context.Hobbies.FirstOrDefault(c => c.Id == id);
            return View(hobbie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hobbie hobbie)
        {
            if (!ModelState.IsValid) return View();
            Hobbie hobieedtit = _context.Hobbies.FirstOrDefault(he => he.Id == hobbie.Id);
            if (hobieedtit == null)
            {
                return NotFound();
            }

            Category SameName = _context.Categories.FirstOrDefault(sm => sm.Name.ToLower().Trim() == hobbie.Name.ToString().Trim());
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {hobbie.Name} alrady taken");
                return View();
            }
            hobieedtit.Name = hobieedtit.Name;
            _context.SaveChanges();


            return RedirectToAction("index", "hobbie");
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            Hobbie Delethobie = _context.Hobbies.FirstOrDefault(dh => dh.Id == id);
            if (Delethobie == null) return Json(new { status = 404 });

            _context.Hobbies.Remove(Delethobie);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
