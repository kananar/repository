using EduHome.Areas.Extensions;
using EduHome.Dal;
using EduHome.Models;
using EduHome.ViewModels;
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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 5);
            List<Teacher> teachers = _context.Teachers.Skip((page - 1) * 5).Take(5).ToList();
            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.faculty = _context.Facultes.ToList();
            ViewBag.Hobbiy = _context.Hobbies.ToList();
            ViewBag.Position = _context.Positions.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemsVM vm)
        {
            ViewBag.faculty = _context.Facultes.ToList();
            ViewBag.Hobbiy = _context.Hobbies.ToList();
            ViewBag.Position = _context.Positions.ToList();
            if (!ModelState.IsValid) return View();
            if (vm.teacher.IMageFile == null)
            {
                ModelState.AddModelError("IMageFile", "image is required");
                return View(vm.teacher);
            }
            vm.teacher.FacultyTeachers = new List<FacultyTeacher>();
            vm.teacher.HobbieTeachers = new List<HobbieTeacher>();
            vm.teacher.PositionTeachers = new List<PositionTeacher>();
            vm.teacher.SocialTeachers = new List<SocialTeacher>();
            vm.teacher.Skills = new List<Skill>();

            for (int i = 0; i < vm.socialTeacher.Icons.Count; i++)
            {
                SocialTeacher socialteacher = new SocialTeacher();
                socialteacher.Icon = vm.socialTeacher.Icons[i];
                socialteacher.Url = vm.socialTeacher.Urls[i];
                socialteacher.Teacher = vm.teacher;
                vm.teacher.SocialTeachers.Add(socialteacher);
            }

            for (int i = 0; i < vm.Skill.SkillNames.Count; i++)
            {
                Skill skill = new Skill();
                skill.Name = vm.Skill.SkillNames[i];
                skill.Point = vm.Skill.SkillPoints[i];
                skill.Teacher = vm.teacher;
                vm.teacher.Skills.Add(skill);
            }





            if (vm.teacher.FacultyIds == null)
            {
                ModelState.AddModelError("FacultyIds", "faculty and department name required");
                return View(vm.teacher);
            }
            else
            {
                foreach (int Id in vm.teacher.FacultyIds)
                {
                    FacultyTeacher facultyTeacher = new FacultyTeacher
                    {
                        FaculteId = Id,
                        Teacher = vm.teacher
                    };
                    vm.teacher.FacultyTeachers.Add(facultyTeacher);
                }
            }
            if (vm.teacher.HobbieIds == null)
            {
                ModelState.AddModelError("HobbieIds", "Hobbie is required");
                return View(vm.teacher);
            }
            else
            {
                foreach (int Id in vm.teacher.HobbieIds)
                {
                    HobbieTeacher hobbieTeacher = new HobbieTeacher
                    {
                        HobbieId = Id,
                        Teacher = vm.teacher
                    };
                    vm.teacher.HobbieTeachers.Add(hobbieTeacher);
                }
            }
            if (vm.teacher.PositionIds == null)
            {
                ModelState.AddModelError("PositionIds", "position is required");
                return View(vm.teacher);
            }
            else
            {
                foreach (int Id in vm.teacher.PositionIds)
                {
                    PositionTeacher positionTeacher = new PositionTeacher
                    {
                        PositionId = Id,
                        Teacher = vm.teacher
                    };
                    vm.teacher.PositionTeachers.Add(positionTeacher);
                }
            }





            if (vm.teacher.IMageFile != null)
            {
                if (!vm.teacher.IMageFile.IsImage())
                {
                    ModelState.AddModelError("IMageFile", "required image file");
                    return View(vm.teacher);
                }
                if (!vm.teacher.IMageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("IMageFile", "must be image file max size 2 mb");
                    return View(vm.teacher);
                }

                vm.teacher.Img = vm.teacher.IMageFile.SaveImage(_env.WebRootPath, "assets/img/teacher");
            }


            _context.Teachers.Add(vm.teacher);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult edit(int id)
        {

            ViewBag.faculty = _context.Facultes.ToList();
            ViewBag.Hobbiy = _context.Hobbies.ToList();
            ViewBag.Position = _context.Positions.ToList();
            ViewBag.Skills = _context.Skills.Where(s => s.TeacherId == id).ToList();
            ViewBag.SocialTeacher = _context.socialTeachers.ToList();

            Teacher teacher = _context.Teachers.Include(t => t.HobbieTeachers).Include(t => t.PositionTeachers).Include(t => t.SocialTeachers).Include(t => t.FacultyTeachers).Include(t => t.Skills).FirstOrDefault(t => t.Id == id);
            return View(teacher);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(Teacher teacher, SocialTeacher socialTeacher)
        {


            ViewBag.faculty = _context.Facultes.ToList();
            ViewBag.Hobbiy = _context.Hobbies.ToList();
            ViewBag.Position = _context.Positions.ToList();
            ViewBag.Skills = _context.Skills.Where(s => s.TeacherId == teacher.Id).ToList();

            Teacher exsistteacher = await _context.Teachers.Include(t => t.HobbieTeachers).Include(t => t.PositionTeachers).Include(t => t.SocialTeachers).Include(t => t.FacultyTeachers).FirstOrDefaultAsync(t => t.Id == teacher.Id);



            if (!ModelState.IsValid) return View(exsistteacher);
            if (exsistteacher == null) return NotFound();


            if (teacher.PositionIds != null)
            {
                List<PositionTeacher> RemovabelePositions = exsistteacher.PositionTeachers.Where(ets => !teacher.PositionIds.Contains(ets.Id)).ToList();
                exsistteacher.PositionTeachers.RemoveAll(pt => RemovabelePositions.Any(rp => rp.Id == pt.Id));


                foreach (var positionId in teacher.PositionIds)
                {
                    PositionTeacher positionTeacher = exsistteacher.PositionTeachers.FirstOrDefault(pt => pt.PositionId == positionId);
                    if (positionTeacher == null)
                    {
                        PositionTeacher position = new PositionTeacher
                        {
                            PositionId = positionId,
                            TeacherId = exsistteacher.Id
                        };
                        exsistteacher.PositionTeachers.Add(position);
                    };
                };
            }
            List<HobbieTeacher> RemovableHobies = exsistteacher.HobbieTeachers.Where(ht => !teacher.PositionIds.Contains(ht.Id)).ToList();
            exsistteacher.HobbieTeachers.RemoveAll(ht => RemovableHobies.Any(rm => rm.Id == ht.Id));

            foreach (var item in teacher.HobbieIds)
            {
                HobbieTeacher hobbieTeacher = exsistteacher.HobbieTeachers.FirstOrDefault(ht => ht.HobbieId == item);
                if (hobbieTeacher == null)
                {
                    HobbieTeacher hobbie = new HobbieTeacher
                    {
                        HobbieId = item,
                        TeacherId = exsistteacher.Id
                    };
                    exsistteacher.HobbieTeachers.Add(hobbie);
                }
            };

            List<FacultyTeacher> removablefaculty = exsistteacher.FacultyTeachers.Where(ft => !teacher.FacultyIds.Contains(ft.Id)).ToList();
            exsistteacher.FacultyTeachers.RemoveAll(ft => removablefaculty.Any(rf => rf.Id == ft.Id));

            foreach (var Faculteid in teacher.FacultyIds)
            {
                FacultyTeacher facultyTeacher = exsistteacher.FacultyTeachers.FirstOrDefault(ft => ft.FaculteId == Faculteid);
                if (facultyTeacher == null)
                {
                    FacultyTeacher faculty = new FacultyTeacher
                    {
                        FaculteId = Faculteid,
                        TeacherId = exsistteacher.Id
                    };
                    exsistteacher.FacultyTeachers.Add(faculty);
                }
            }



            List<Skill> Removableskils = exsistteacher.Skills.Where(st => !teacher.SkillNames.Contains(st.Name)).ToList();
            exsistteacher.Skills.RemoveAll(st => Removableskils.Any(rb => rb.Name == st.Name));

            for (int i = 0; i < teacher.SkillNames.Count; i++)
            {
                Skill skil1 = exsistteacher.Skills.FirstOrDefault(s => s.Name == teacher.SkillNames[i]);
                if (skil1 == null)
                {
                    Skill skill = new Skill();
                    skill.Name = teacher.SkillNames[i];
                    skill.Point = teacher.SkillPoints[i];
                    skill.TeacherId = exsistteacher.Id;
                    exsistteacher.Skills.Add(skill);
                }


            }


            List<SocialTeacher> Removablesocials = exsistteacher.SocialTeachers.Where(st => !teacher.Icons.Contains(st.Icon)).ToList();
            exsistteacher.SocialTeachers.RemoveAll(st => Removablesocials.Any(rb => rb.Icon == st.Icon));


            for (int i = 0; i < teacher.Icons.Count; i++)
            {
                SocialTeacher social = exsistteacher.SocialTeachers.FirstOrDefault(st => st.Icon == teacher.Icons[i]);
                if (social == null)
                {
                    SocialTeacher socialteacher = new SocialTeacher();
                    socialteacher.Icon = teacher.Icons[i];
                    socialteacher.Url = teacher.Urls[i];
                    socialteacher.TeacherId = exsistteacher.Id;
                    exsistteacher.SocialTeachers.Add(socialteacher);
                }



            }

            if (teacher.IMageFile!=null)
            {
                if (!teacher.IMageFile.IsImage())
                {
                    ModelState.AddModelError("IMageFile", "required image file");
                }
                if (!teacher.IMageFile.IsSizeOk(2))
                {
                    ModelState.AddModelError("IMageFile", "required image file max size 2 mb");
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", exsistteacher.Img);
                exsistteacher.Img = teacher.IMageFile.SaveImage(_env.WebRootPath, "assets/img/teacher");
            }
            exsistteacher.Name = teacher.Name;
            exsistteacher.Surname = teacher.Surname;
            exsistteacher.Mail = teacher.Mail;
            exsistteacher.PhoneNUmber = teacher.PhoneNUmber;
            exsistteacher.Experience = teacher.Experience;
            exsistteacher.AboutMe = teacher.AboutMe;
            exsistteacher.Degree = teacher.Degree;
            


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null) return Json(new { status = 404 });
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", teacher.Img);
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            
            return Json(new { status = 200 });
        }


    }
}
