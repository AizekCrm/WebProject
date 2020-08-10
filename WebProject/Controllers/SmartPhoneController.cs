using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebProject.Interface;
using WebProject.Models;
using WebProject.ViewModel;

namespace WebProject.Controllers
{
    [Authorize(Roles ="admin, user")]
    public class SmartPhoneController : Controller
    {
        private readonly IAllMobile iPhone;

        public SmartPhoneController(IAllMobile iPhone)
        {
            this.iPhone = iPhone;
        }

        [HttpGet("SmartPhone/SmartPhone")]
        public IActionResult SmartPhone()
        {
            var Pv = new PhoneView
            {
                AllPhone = iPhone.Mobiles
            };
            return View(Pv);
        }

        [HttpPost("SmartPhone/SearchPhone")]
        public IActionResult SearchPhone(string company)
        {
            if (string.IsNullOrEmpty(company))
            {
                return RedirectToAction("SmartPhone");
            }
            else
            {
                var NewPhone = new PhoneView
                {
                    AllPhone = iPhone.Mobiles.Where(c => c.Company == company)
                };
                return View(NewPhone);
            }
        }
    }
}
