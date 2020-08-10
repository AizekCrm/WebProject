using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Models.UserModel;
using WebProject.Models.UserModel.Role;
using WebProject.ViewModel;

namespace WebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly MobileContext db;
        public AccountController(MobileContext context)
        {
            db = context;
        }

        [HttpGet("Account/FormLog")]
        public IActionResult FormLog()
        {
            return View();
        }

        [HttpPost("Account/FormLog")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormLog(UserLog _userLog)
        {
            if ((_userLog.Login == _userLog.Password) && (!string.IsNullOrEmpty(_userLog.Login) && !string.IsNullOrEmpty(_userLog.Password)))
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = await db.Users
                    .Include(v => v.Role)
                    .FirstOrDefaultAsync(u => u.Login == _userLog.Login && u.Password == _userLog.Password);
                if(user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index","Index");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(_userLog);
        }

        [HttpGet("Account/FormReg")]
        public IActionResult FormReg ()
        {
            return View();
        }

        [HttpPost("Account/FormReg")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormReg (UserReg _userReg)
        {
            if ((_userReg.Login == _userReg.Password) && (!string.IsNullOrEmpty(_userReg.Login) && !string.IsNullOrEmpty(_userReg.Password)))
            {
                ModelState.AddModelError("", "Логин и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(v => v.Login == _userReg.Login);
                if (user == null)
                {
                    user = new User {
                        Login = _userReg.Login,
                        Password = _userReg.Password
                    };
                    RoleUser roleUs = await db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (roleUs != null)
                        user.Role = roleUs;
                    await db.Users.AddAsync(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("FormLog","Account");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(_userReg);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet("Account/LogOut")]
        public async Task<IActionResult> LogOut ()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("FormLog","Account");
        }
    }
}
