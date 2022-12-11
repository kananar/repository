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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context ,IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            Setting setting = _context.Setting.FirstOrDefault();
            return View(setting);
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Setting.Include(s=>s.SociamMediaSettings).FirstOrDefault(s=>s.Id==id);
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            Setting settingEdit = _context.Setting.Include(s => s.SociamMediaSettings).FirstOrDefault(s => s.Id == setting.Id);
            
            if (!ModelState.IsValid) return View(settingEdit);
          
          
            if (setting.LogoFile !=null && setting.FootorlogoFile !=null && setting.InfoImgFile !=null)
            {
                if (!setting.LogoFile.IsImage())
                {
                    ModelState.AddModelError("LogoFlie", "file must be image file");
                    return View();
                }
                if (!setting.LogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("LogoFlie", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.FootorlogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FootorlogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.InfoImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.InfoImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("InfoImgFile", "file must be max size 2mb ");
                    return View();
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.Logo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.InfoImg);
                settingEdit.Logo = setting.LogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
                settingEdit.FooterLogo = setting.FootorlogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
                settingEdit.InfoImg = setting.InfoImgFile.SaveImage(_env.WebRootPath, "assets/img/about");
               
            }
            if (setting.LogoFile == null && setting.FootorlogoFile != null && setting.InfoImgFile != null)
            {
                
                if (!setting.FootorlogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FootorlogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.InfoImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.InfoImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("InfoImgFile", "file must be max size 2mb ");
                    return View();
                }

               
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.InfoImg);
               
                settingEdit.FooterLogo = setting.FootorlogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
                settingEdit.InfoImg = setting.InfoImgFile.SaveImage(_env.WebRootPath, "assets/img/about");

            }
            if (setting.LogoFile != null && setting.FootorlogoFile == null && setting.InfoImgFile != null)
            {
                if (!setting.LogoFile.IsImage())
                {
                    ModelState.AddModelError("LogoFlie", "file must be image file");
                    return View();
                }
                if (!setting.LogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("LogoFlie", "file must be max size 2mb ");
                    return View();
                }
            
                if (!setting.InfoImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.InfoImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("InfoImgFile", "file must be max size 2mb ");
                    return View();
                }

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.Logo);
               
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.InfoImg);
                settingEdit.Logo = setting.LogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
                
                settingEdit.InfoImg = setting.InfoImgFile.SaveImage(_env.WebRootPath, "assets/img/about");

            }
            if (setting.LogoFile != null && setting.FootorlogoFile != null && setting.InfoImgFile == null)
            {
                if (!setting.LogoFile.IsImage())
                {
                    ModelState.AddModelError("LogoFlie", "file must be image file");
                    return View();
                }
                if (!setting.LogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("LogoFlie", "file must be max size 2mb ");
                    return View();
                }
                if (!setting.FootorlogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FootorlogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
           

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.Logo);
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
               
                settingEdit.Logo = setting.LogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
                settingEdit.FooterLogo = setting.FootorlogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
               

            }
            if (setting.LogoFile == null && setting.FootorlogoFile == null && setting.InfoImgFile != null)
            {
              
            
                if (!setting.InfoImgFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.InfoImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("InfoImgFile", "file must be max size 2mb ");
                    return View();
                }

                
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/about", settingEdit.InfoImg);
                
                settingEdit.InfoImg = setting.InfoImgFile.SaveImage(_env.WebRootPath, "assets/img/about");

            }
            if (setting.LogoFile != null && setting.FootorlogoFile == null && setting.InfoImgFile == null)
            {
                if (!setting.LogoFile.IsImage())
                {
                    ModelState.AddModelError("LogoFlie", "file must be image file");
                    return View();
                }
                if (!setting.LogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("LogoFlie", "file must be max size 2mb ");
                    return View();
                }
               

                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.Logo);
               
                settingEdit.Logo = setting.LogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
               

            }
            if (setting.LogoFile == null && setting.FootorlogoFile != null && setting.InfoImgFile == null)
            {
              
                if (!setting.FootorlogoFile.IsImage())
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be image file");
                    return View();
                }
                if (!setting.FootorlogoFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("FootorlogoFile", "file must be max size 2mb ");
                    return View();
                }
                
                
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", settingEdit.FooterLogo);
               
               
                settingEdit.FooterLogo = setting.FootorlogoFile.SaveImage(_env.WebRootPath, "assets/img/logo");
               

            }

            settingEdit.InfoTitle = setting.InfoTitle;
            settingEdit.InfoDescriptionBottom = setting.InfoDescriptionBottom;
            settingEdit.InfoDescriptionTop = setting.InfoDescriptionTop;
            settingEdit.FooterDedcription = setting.FooterDedcription;
            settingEdit.Address  = setting.Address;
            settingEdit.WebAddress = setting.WebAddress;
            settingEdit.Mail = setting.Mail;
            settingEdit.PhoneNumber1 = setting.PhoneNumber1;
            settingEdit.PhoneNumber2 = setting.PhoneNumber2;
            settingEdit.VideoUrl = setting.VideoUrl;
            settingEdit.SearchIcon = setting.SearchIcon;
            _context.SaveChanges();


            return RedirectToAction("index","setting");
        }

        
    }

}
