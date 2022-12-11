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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Appuser> _userManager;

        public EventController(AppDbContext context,UserManager<Appuser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 6);
            ViewBag.Categorys = _context.Categories.ToList();

            List<Event> events = _context.Events.Skip((page - 1) * 2).Take(6).ToList();
            return View(events);

        }

        [HttpGet]
        public async Task<IActionResult> Index(string EventSearch, int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 6);
            ViewBag.Categorys = _context.Categories.ToList();

            List<Event> events = _context.Events.Skip((page - 1) * 2).Take(6).ToList();
           
            if (!string.IsNullOrEmpty(EventSearch))
            {
                ViewData["GetEventDetails"] = EventSearch;

                var result = from x in _context.Events select x;
                result = result.Where(x => x.Title.Contains(EventSearch) || x.Description.Contains(EventSearch));
                return View(await result.AsNoTracking().ToListAsync());
            }
            return View(events);
        }

        public IActionResult Details(int Id)
        {
            HomeVM home = new HomeVM
            {
                eevent = _context.Events.Include(c => c.EventSpeakers).ThenInclude(s => s.Spekaer).Include(e=>e.Comments).FirstOrDefault(e => e.Id == Id),
                Speakers = _context.Speakers.Where(s => s.EventSpeakers.Any(es => es.EventId == Id)).Include(s=>s.SpeakerPositions).ThenInclude(es=>es.Position).ToList(),
                appusers = _context.Users.ToList()

            };
            return View(home);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> AddComment(Comment comment)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //return Content(user.Id);
            if (!ModelState.IsValid) return RedirectToAction("Deteails", "Event", new { id = comment.EventId });
            if (!_context.Events.Any(f => f.Id == comment.EventId)) return NotFound();
            Comment Newcomment = new Comment
            {
                Message = comment.Message,
                EventId = comment.EventId,
                AppuserId = user.Id,
                Date = DateTime.Now
            };

            _context.Comments.Add(Newcomment);
            await _context.SaveChangesAsync();

            return RedirectToAction("details", "Event", new { id = comment.EventId });

        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("details", "Event");
            if (!_context.Comments.Any(c=> c.AppuserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppuserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "event", new { id = comment.EventId });
        }


    }
}



