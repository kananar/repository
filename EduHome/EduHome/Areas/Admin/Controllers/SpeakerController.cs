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
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SpeakerController(AppDbContext context, IWebHostEnvironment env)
        {
           _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Speaker> speakers = _context.Speakers.Include(c => c.SpeakerPositions).ThenInclude(pc => pc.Speaker).ToList();

            return View(speakers);
        }

        public IActionResult Create()
        {
           

            ViewBag.Position = _context.Positions.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker)
        {
            ViewBag.Position = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(speaker);
            if (speaker.ImgFile!=null)
            {
                if (!speaker.ImgFile.IsImage())
                {
                    ModelState.AddModelError("ImgFile", "required image file");
                    return View();
                }
                if (!speaker.ImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImgFile", " image file max size must be 2 mb");
                    return View();
                }

                speaker.Img = speaker.ImgFile.SaveImage(_env.WebRootPath, "assets/img/event");
            }
            else
            {
                ModelState.AddModelError("ImgFile", "Image cannot be null");
                return View();
            }
            if (speaker.PositionIds==null)
            {
                ModelState.AddModelError("PositionIds", "Position can not be null");
                return View();
            }
            else
            {
                foreach (var id in speaker.PositionIds)
                {
                    SpeakerPosition speakerPosition = new SpeakerPosition
                    {
                        PositionId=id,
                        Speaker=speaker
                        
                    };
                    _context.SpeakerPositions.Add(speakerPosition);
                }
                
            }
            _context.Speakers.Add(speaker);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            Speaker speakers = _context.Speakers.Include(s=>s.SpeakerPositions).ThenInclude(sp=>sp.Position).FirstOrDefault(s=>s.Id==Id);

            ViewBag.Position = _context.Positions.ToList();
            return View(speakers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Speaker speaker)
        {
            ViewBag.Position = _context.Positions.ToList();
            Speaker ExsistSpeaker = _context.Speakers.Include(es => es.SpeakerPositions).ThenInclude(sp => sp.Position).FirstOrDefault(es => es.Id == speaker.Id);
            if (!ModelState.IsValid) return View(ExsistSpeaker);
            
           
            if (ExsistSpeaker==null)
            {
                return NotFound();
            }
            if (speaker.ImgFile!=null)
            {
                if (!speaker.ImgFile.IsImage())
                {
                    ModelState.AddModelError("ImgFile", "Please choose image file");
                    return View(ExsistSpeaker);
                }
                if (!speaker.ImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImgFile", "Please choose image file");
                    return View(ExsistSpeaker);
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", ExsistSpeaker.Img);
                ExsistSpeaker.Img = speaker.ImgFile.SaveImage(_env.WebRootPath, "assets/img/event");
            }
            if (speaker.PositionIds!=null)
            {
                List<SpeakerPosition> removableposition = _context.SpeakerPositions.Where(rp => !speaker.PositionIds.Contains(rp.Id)).ToList();
                ExsistSpeaker.SpeakerPositions.RemoveAll(sp => removableposition.Any(rm => rm.Id == sp.Id));

                foreach (var id in speaker.PositionIds)
                {
                    SpeakerPosition speakerPosition = ExsistSpeaker.SpeakerPositions.FirstOrDefault(sp => sp.PositionId == id);
                    if (speakerPosition == null)
                    {
                        SpeakerPosition speakerPosition1 = new SpeakerPosition
                        {
                            PositionId = id,
                            SpeakreId = ExsistSpeaker.Id
                        };
                        ExsistSpeaker.SpeakerPositions.Add(speakerPosition1);
                    }
                }
            }

            
            
            ExsistSpeaker.Name = speaker.Name;
            ExsistSpeaker.SurnameName = speaker.SurnameName;
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Speaker speaker = _context.Speakers.Include(s=>s.SpeakerPositions).ThenInclude(sp=>sp.Position).FirstOrDefault(s => s.Id == id);
            if (speaker == null) return Json(new { status=404});
            _context.Speakers.Remove(speaker);
            _context.SaveChanges();
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", speaker.Img);
            return Json(new { status = 200 });
            
        }
    }
}
