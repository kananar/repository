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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 3);
            List<Event> events = _context.Events.Skip((page - 1) * 3).Take(3).ToList();
            return View(events);
        }


        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event eevent)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            if (!ModelState.IsValid) return View();

            if (eevent.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
            if (eevent.SpeakersIds == null)
            {
                ModelState.AddModelError("SpeakersIds", "Speaker is required");
                return View();
            }

            if (eevent.ImageFile != null)
            {
                if (!eevent.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Image file  required");
                    return View();
                }
                if (!eevent.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "Image file max size must be 2mb ");
                    return View();
                }

                eevent.Img = eevent.ImageFile.SaveImage(_env.WebRootPath, "assets/img/event");
            }

            if (eevent.SpeakersIds != null)
            {
                foreach (var item in eevent.SpeakersIds)
                {
                    EventSpeaker eventSpeaker = new EventSpeaker
                    {
                        SpekaerId = item,
                        Event = eevent
                    };
                    _context.EventSpeakers.Add(eventSpeaker);


                }
            }

            _context.Events.Add(eevent);
            _context.SaveChanges();
            return RedirectToAction("index", "event");
        }

        public IActionResult Edit(int id)
        {
            Event eevent = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Spekaer).FirstOrDefault(e => e.Id == id);
            ViewBag.Speakers = _context.Speakers.ToList();
            return View(eevent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event eevent)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            Event ExsistEvent = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Spekaer).FirstOrDefault(e => e.Id == eevent.Id);
            if (!ModelState.IsValid) return View(ExsistEvent);
            
            if (ExsistEvent == null)
            {
                return NotFound();
            }
            if (eevent.ImageFile != null)
            {
                if (!eevent.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "please choose image file");
                    return View(ExsistEvent);
                }
                if (!eevent.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImageFile", "IMAGE SIZE MUST BE MAX 2MB");
                    return View(ExsistEvent);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", ExsistEvent.Img);
                ExsistEvent.Img = eevent.ImageFile.SaveImage(_env.WebRootPath, "assets/img/event");
            }

            if (eevent.SpeakersIds != null)
            {
                List<SpeakerPosition> RemovabelSpeaker = _context.SpeakerPositions.Where(sp => !eevent.SpeakersIds.Contains(sp.Id)).ToList();

                ExsistEvent.EventSpeakers.RemoveAll(es => RemovabelSpeaker.Any(rs => rs.Id == es.Id));
                foreach (var speaker in eevent.SpeakersIds)
                {
                    EventSpeaker eventSpeaker = ExsistEvent.EventSpeakers.FirstOrDefault(es => es.SpekaerId == speaker);
                    EventSpeaker eventSpeaker1 = new EventSpeaker
                    {
                        SpekaerId=speaker,
                        EventId=ExsistEvent.Id
                    };

                    ExsistEvent.EventSpeakers.Add(eventSpeaker1);
                }


            }
            ExsistEvent.Description = eevent.Description;
            ExsistEvent.Title = eevent.Title;
            ExsistEvent.Venue = eevent.Venue;
            _context.SaveChanges();



            return RedirectToAction(nameof(Index));
        }
        

        public IActionResult Delete(int id)
        {
            Event eevent = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eevent == null) return Json(new { status = 404 });
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", eevent.Img);
            _context.Events.Remove(eevent);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
    

}
