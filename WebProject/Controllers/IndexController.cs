using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebProject.Models;


namespace WebProject.Controllers
{
    [Authorize(Roles ="admin , user")]
    public class IndexController : Controller
    {
        [HttpGet("")]
        [HttpGet("Index/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
