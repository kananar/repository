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
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Position> positions = _context.Positions.ToList();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid) return View();
            if (position.Name == null)
            {
                ModelState.AddModelError("Name", "position Name Is required");
            }
            Position SameName = _context.Positions.FirstOrDefault(sm => sm.Name.ToLower().Trim() == position.Name.ToString().Trim());
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {SameName.Name} alrady taken");
                return View();
            }

            _context.Positions.Add(position);
            _context.SaveChanges();

            return RedirectToAction("index", "position");
        }

        public IActionResult Edit(int id)
        {
            Position position = _context.Positions.FirstOrDefault(c => c.Id == id);
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Position position)
        {
            if (!ModelState.IsValid) return View();
            Position positionEdit = _context.Positions.FirstOrDefault(ce => ce.Id == position.Id);
            if (position == null)
            {
                return NotFound();
            }

            Position SameName = _context.Positions.FirstOrDefault(sm => sm.Name.ToLower().Trim() == position.Name.ToString().Trim());
            if (SameName != null)
            {
                ModelState.AddModelError("", $"this Name {position.Name} alrady taken");
                return View();
            }
            positionEdit.Name = position.Name;
            _context.SaveChanges();


            return RedirectToAction("index", "position");
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            Position DeletPosition = _context.Positions.FirstOrDefault(dc => dc.Id == id);
            if (DeletPosition == null) return Json(new { status = 404 });

            _context.Positions.Remove(DeletPosition);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
