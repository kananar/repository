using EduHome.Dal;
using EduHome.Models;
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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Appuser> _userManager;

        public BlogController(AppDbContext context, UserManager<Appuser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 9);
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 2).Take(12).ToList();
            ViewBag.blogs = _context.Blogs.OrderByDescending(b => b.DateTime).Take(2).ToList();

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string BlogSearch, int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 9);
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 2).Take(12).ToList();
            ViewBag.blogs = _context.Blogs.OrderByDescending(b => b.DateTime).Take(2).ToList();

            

            if (!string.IsNullOrEmpty(BlogSearch))
            {
                ViewData["GetBlogDetails"] = BlogSearch;

                var result = from x in _context.Blogs select x;
               
                    result = result.Where(x => x.Title.Contains(BlogSearch) || x.Description.Contains(BlogSearch));
                
                return View(await result.AsNoTracking().ToListAsync());
            }
            return View(blogs);
        }

        public IActionResult Details(int Id)
        {
            Blog blog = _context.Blogs.Include(b=>b.Comments).ThenInclude(c=>c.Appuser).FirstOrDefault(b => b.Id == Id);
            ViewBag.blogs = _context.Blogs.OrderByDescending(b => b.DateTime).Take(2).ToList();
            return View(blog);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            //return Content(user.Id);
            if (!ModelState.IsValid) return RedirectToAction("Deteails", "Blog", new { id = comment.BlogId });
            if (!_context.Blogs.Any(f => f.Id == comment.BlogId)) return NotFound();
            Comment Newcomment = new Comment
            {
                Message = comment.Message,
                BlogId = comment.BlogId,
                AppuserId = user.Id,
                Date = DateTime.Now
            };

            _context.Comments.Add(Newcomment);
            await _context.SaveChangesAsync();

            return RedirectToAction("details", "Blog", new { id = comment.BlogId });

        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Appuser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("details", "blog");
            if (!_context.Comments.Any(c => c.AppuserId == user.Id)) return NotFound();
            Comment comment = _context.Comments.FirstOrDefault(c => c.Id == id && c.AppuserId == user.Id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("details", "blog", new { id = comment.BlogId });
        }
    }
}
