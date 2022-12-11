using EduHome.Dal;
using EduHome.Enum;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Appuser> _userManager;
        private readonly SignInManager<Appuser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController( UserManager<Appuser> userManager,SignInManager<Appuser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ForgetPasword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPasword(AccountVM account)
        {
            Appuser user = await _userManager.FindByEmailAsync(account.Appuser.Email);
            if (user == null) return BadRequest();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string Link = Url.Action(nameof(ResetPasword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("homeedu91@gmail.com", "eduhome");
            mail.To.Add(user.Email);
            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{Link}'>Plaese click gere for reset password<a/>";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("homeedu91@gmail.com", "072719991");
            smtp.Send(mail);
            return RedirectToAction("index", "home");
            
         
        }

        public async Task<IActionResult> ResetPasword(string email,string token)
        {
            Appuser appuser = await _userManager.FindByEmailAsync(email);
            if (appuser == null) return BadRequest();
            AccountVM account = new AccountVM
            {
              Appuser=appuser,
              Token=token
            };
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPasword(AccountVM account)
        {
            //return Json(account.Token);
            Appuser user = await _userManager.FindByEmailAsync(account.Appuser.Email);

        

            AccountVM model = new AccountVM
            {
                Appuser = account.Appuser,
                Token = account.Token
            };
            if (!ModelState.IsValid) return View(model);
            IdentityResult Result =await _userManager.ResetPasswordAsync(user, account.Token, account.Password);
            if (!Result.Succeeded)
            {
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(model);
            }
            return RedirectToAction("index", "home");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult > Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            if (!register.Terms)
            {
                ModelState.AddModelError("Terms", "please check");
            }
            Appuser user = new Appuser
            {
                Name=register.Name,
                UserName=register.UserName,
                Surname=register.SurName,
                Email=register.Email,
                Roles=RoleEnum.User.ToString(),
                Isadmin=false

            };
      


            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", $"{error.Description}");
                }
                return View();
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string link = Url.Action(nameof(VerfiyEmail),"Account",new { email=user.Email,token },Request.Scheme,Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("homeedu91@gmail.com", "Eduhome");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "VerifyEmail";

            string body = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/Template/Verify.html"))
            {
                body = stream.ReadToEnd();
            }

            string Info = $"welcome{user.Name + " " + user.Surname}";
            body = body.Replace("{{link}}", link);
            mail.Body = body.Replace("{{info}}", Info);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("homeedu91@gmail.com", "072719991");
            smtp.Send(mail);
          
            await _userManager.AddToRoleAsync(user, RoleEnum.User.ToString());

            TempData["Verify"] = true;
            TempData["Veirfy"] = true;



            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> VerfiyEmail(string email,string token)
        {
            Appuser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            TempData["Verified"] = true;
            return RedirectToAction("Index","Home");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {

            
            if (login.UserName==null)
            {
                ModelState.AddModelError("", "name or pasword incorrect");
                return View();
            }
            if (login.Password == null)
            {
                ModelState.AddModelError("", "name or pasword incorrect");
                return View();
            }
            if (!ModelState.IsValid) return View();
            Appuser user = await _userManager.FindByNameAsync(login.UserName);
            
            if (user==null)
            {
                ModelState.AddModelError("", "UserName or Password incorrect");
                return View();
            }
            if (user.Isadmin)
            {
                ModelState.AddModelError("", "username or pasword incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult Result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, login.RememberMe, true);
           
            if (!Result.Succeeded)
            {
                if (Result.IsLockedOut)
                {
                    ModelState.AddModelError("", "your account blocked 5 minute");
                    return View();
                }
                ModelState.AddModelError("", "UserName or Password Incorrect");
                return View();
            }


            return RedirectToAction("Index","Home");

           
        }

        public async Task<IActionResult> edit()
        {
            Appuser user =await _userManager.FindByNameAsync(User.Identity.Name);
            EditVM edit = new EditVM
            {
                UserName=user.UserName,
                Name=user.Name,
                SurName=user.Surname,
                Email=user.Email
            };
            return View(edit);
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(EditVM edit)
        {
            if (edit.UserName==null)
            {
                ModelState.AddModelError("UserName", "required");
                return View();
            }
            if (edit.Name == null)
            {
                ModelState.AddModelError("Name", "required");
                return View();
            }
            if (edit.SurName == null)
            {
                ModelState.AddModelError("SurName", "required");
                return View();
            }
            if (edit.Email == null)
            {
                ModelState.AddModelError("Email", "required");
                return View();
            }

            Appuser appuser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return View(appuser);

            EditVM EditUser = new EditVM
            {
                UserName = edit.UserName,
                Name = edit.Name,
                SurName = edit.SurName,
                Email = edit.Email
            };
            if (appuser.UserName!=edit.UserName && await _userManager.FindByNameAsync(edit.UserName)!=null)
            {
                ModelState.AddModelError("", $"this name{edit.UserName} alrady taken");
                return View(EditUser);
            }

            if (string.IsNullOrEmpty(edit.CurrentPasword))
            {

                appuser.UserName = edit.UserName;
                appuser.Name = edit.Name;
                appuser.Surname = edit.SurName;
                appuser.Email = edit.Email;
                await _userManager.UpdateAsync(appuser);
                await _signInManager.SignInAsync(appuser, true);
            }
            else
            {
                appuser.UserName = edit.UserName;
                appuser.Name = edit.Name;
                appuser.Surname = edit.SurName;
                appuser.Email = edit.Email;

                IdentityResult IdentityResult = await _userManager.ChangePasswordAsync(appuser, edit.CurrentPasword, edit.Password);
                if (!IdentityResult.Succeeded)
                {
                    foreach (var error in IdentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(EditUser);
                }

                await _signInManager.PasswordSignInAsync(appuser, edit.Password, true, true);
            }
            return RedirectToAction("Index", "Home");
        }
        



        public IActionResult Show()
        {
            return Content(User.Identity.Name);
        }

        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
