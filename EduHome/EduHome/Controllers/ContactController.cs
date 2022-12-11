using EduHome.Dal;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Setting = _context.Setting.FirstOrDefault()
            };
            return View(home);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(Contact contact1)
        {
           

            if (contact1.Email == null)
            {
                ModelState.AddModelError("Email", "email is required");
            }
            if (contact1.Text == null)
            {
                ModelState.AddModelError("Text", "Text is required");
                return RedirectToAction(nameof(Index));

            }
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));

            Contact contact = new Contact
            {
                Email = contact1.Email,
                Text = contact1.Text,
            };
            
          
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
           
        }
    }
}
