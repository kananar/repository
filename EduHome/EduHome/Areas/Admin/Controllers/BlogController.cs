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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 5);
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 5).Take(5).ToList();
            return View(blogs);
        }

        public IActionResult Create()
        {
         
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Blog blog)
        {
           
            //if (!ModelState.IsValid) return View();
            if (blog.ImgFile == null)
            {
                ModelState.AddModelError("ImgFile", "Image is required");
                return View();
            }
            else
            {
                if (!blog.ImgFile.IsImage())
                {
                    ModelState.AddModelError("ImgFile", "please choose image file");
                    return View ();
                }
                if (!blog.ImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImgFile", "image size must be max 2 mb");
                    return View();

                }
                blog.Img = blog.ImgFile.SaveImage(_env.WebRootPath, "assets/img/blog");
            }
            
            _context.Blogs.Add(blog);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public  IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blog blog)
        {
            Blog EditBlog = await _context.Blogs.FirstOrDefaultAsync(c => c.Id == blog.Id);
            if (!ModelState.IsValid) return View(EditBlog);
            if (EditBlog==null)
            {
                return NotFound();
            }
            if (blog.ImgFile !=null)
            {
                if (!blog.ImgFile.IsImage())
                {
                    ModelState.AddModelError("ImgFile", "required image file");
                    return View();
                }
                if (!blog.ImgFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("ImgFile", " image size must be max size 2mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", EditBlog.Img);
                EditBlog.Img = blog.ImgFile.SaveImage(_env.WebRootPath, "assets/img/blog");
            }
            EditBlog.Title = blog.Title;
            EditBlog.Description = blog.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        
        public IActionResult Delete(int Id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == Id);
            if (blog == null) return Json(new {status=404 });
            _context.Blogs.Remove(blog);
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", blog.Img);
            _context.SaveChanges();
            return Json(new { status = 200 });


        }
        
    }
}
