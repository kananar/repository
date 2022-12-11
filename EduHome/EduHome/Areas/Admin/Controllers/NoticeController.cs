using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class NoticeController : Controller
    {
        private readonly AppDbContext _context;

        public NoticeController(AppDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
           List< NoticeVioard> noticeVioard = _context.NoticeVioards.ToList();
            return View(noticeVioard);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeVioard noticeVioard)
        {
            if (!ModelState.IsValid) return View(noticeVioard);
            noticeVioard.Time= DateTime.Now;
            _context.NoticeVioards.Add(noticeVioard);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            NoticeVioard notice = _context.NoticeVioards.FirstOrDefault(nv => nv.Id==id);
            if (notice == null) return NotFound();
            return View(notice);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeVioard noticeVioard)
        {
            NoticeVioard ExsistNotice = _context.NoticeVioards.FirstOrDefault(d=>d.Id==noticeVioard.Id);
            if (!ModelState.IsValid) return View(ExsistNotice);
            ExsistNotice.Title = noticeVioard.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Delete(int Id)
        {
            NoticeVioard notice = _context.NoticeVioards.FirstOrDefault(f => f.Id == Id);
            if (notice == null) return Json(new { status = 404 });
            _context.NoticeVioards.Remove(notice);
            _context.SaveChanges();
            return Json(new { status = 200 });

        }
    }
}
