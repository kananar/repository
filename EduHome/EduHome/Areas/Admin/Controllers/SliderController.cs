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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> Sliders = _context.Sliders.OrderBy(o => o.Order).ToList();
            return View(Sliders);
        }

        public IActionResult Creat()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creat(Slider slider)
        {
            List<Slider> sliders = _context.Sliders.ToList();
            if (!ModelState.IsValid) return View();
           
            if (slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image Required");
                return View();
            }
            
            if (!slider.ImageFile.IsImage())
            {
                ModelState.AddModelError("IimageFile", "please Choose Image File");
                return View();
            }
            if (!slider.ImageFile.IsSizeOk(2))
            {
                ModelState.AddModelError("IimageFile", "Image size must be max 2mb ");
                return View();
            }
            if (slider.Descrtiption==null)
            {
                ModelState.AddModelError("", "description is ruqaired");
            }

            slider.SliderImg = slider.ImageFile.SaveImage(_env.WebRootPath, "assets/img/slider");
            slider.Order = sliders.Count + 1;
            _context.Sliders.Add(slider);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == Id);
            if (slider == null)    return Json(new { status = 404 });
            
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/slider",slider.SliderImg);
                _context.Sliders.Remove(slider);
                _context.SaveChanges();

            return Json(new { status=200 });
        }

        public IActionResult Edit(int Id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == Id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            Slider EditSlider = _context.Sliders.FirstOrDefault(s => s.Id == slider.Id);
            if (EditSlider == null) return NotFound();
            if (!ModelState.IsValid) return View(EditSlider);
            if (slider.ImageFile!=null)
            {

                if (!slider.ImageFile.IsImage())
                {
                    ModelState.AddModelError("IimageFile", "please Choose Image File");
                    return View(EditSlider);
                }
                if (!slider.ImageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("IimageFile", "Image size must be max 2mb ");
                    return View(EditSlider);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/slider", EditSlider.SliderImg);
                EditSlider.SliderImg = slider.ImageFile.SaveImage(_env.WebRootPath, "assets/img/slider");
            }
            if (slider.Title==null)
            {
                ModelState.AddModelError("Title", "Title is Required");
                return View(EditSlider);
            }
            if (slider.Descrtiption==null)
            {
                ModelState.AddModelError("Description", "Description is required");
                return View(EditSlider);
            }
            EditSlider.Title = slider.Title;
            EditSlider.Descrtiption = slider.Descrtiption;
           
            _context.SaveChanges();

            return RedirectToAction("index","slider");
        }
    }
}
