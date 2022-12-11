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
    public class TagController : Controller
    {
        public AppDbContext _context { get; }

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Tag> tags = _context.Tags.ToList();
            return View(tags);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid) return View();
            if (tag.Name == null)
            {
                ModelState.AddModelError("Name", "Category Name Is required");
            }
            Tag SameName = _context.Tags.FirstOrDefault(sm => sm.Name.ToLower().Trim() == tag.Name.ToString().Trim());
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {tag.Name} alrady taken");
                return View();
            }

            _context.Tags.Add(tag);
            _context.SaveChanges();

            return RedirectToAction("index", "tag");
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            Tag DeletTag = _context.Tags.FirstOrDefault(dc => dc.Id == id);
            if (DeletTag == null) return Json(new { status = 404 });

            _context.Tags.Remove(DeletTag);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
