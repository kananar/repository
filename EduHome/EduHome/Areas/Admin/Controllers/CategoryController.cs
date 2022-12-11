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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

     
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 5);
            List<Category> categories = _context.Categories.Skip((page - 1) * 5).Take(5).ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            if (category.Name==null)
            {
                ModelState.AddModelError("Name", "Category Name Is required");
            }
            Category SameName = _context.Categories.FirstOrDefault(sm => sm.Name.ToLower().Trim() == category.Name.ToString().Trim());
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {category.Name} alrady taken");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("index","category");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid) return View();
            Category categoryEdit = _context.Categories.FirstOrDefault(ce => ce.Id == category.Id);
            if (category==null)
            {
                return NotFound();
            }

            Category SameName = _context.Categories.FirstOrDefault(sm => sm.Name.ToLower().Trim() == category.Name.ToString().Trim());
            if (SameName !=null)
            {
                ModelState.AddModelError("", $"this Name {category.Name} alrady taken");
                return View();
            }
            categoryEdit.Name = category.Name;
            _context.SaveChanges();


            return RedirectToAction("index","category");
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            Category DeletCategory = _context.Categories.FirstOrDefault(dc => dc.Id == id);
            if (DeletCategory == null) return Json(new { status = 404 });

            _context.Categories.Remove(DeletCategory);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
