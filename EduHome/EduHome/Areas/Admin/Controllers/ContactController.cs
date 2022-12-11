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
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Contact> contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        public IActionResult Delete(int Id)
        {
            Contact contact = _context.Contacts.FirstOrDefault(c => c.Id == Id);
            if (contact == null) return NotFound();
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
