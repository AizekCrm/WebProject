using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    [Authorize(Roles ="admin")]
    public class FormController : Controller
    {

        [HttpPost("Form/Area")]
        public string Area()
        {
            string altitudeString = Request.Form.FirstOrDefault(p => p.Key == "altitude").Value;
            int altitude = int.Parse(altitudeString);

            string heightString = Request.Form.FirstOrDefault(p => p.Key == "height").Value;
            int height = int.Parse(heightString);

            double square = altitude * height / 2;
            return $"Площадь треугольника с основанием {altitude} и высотой {height} равна {square}";
        }

        [HttpGet("Form/Form")]
        public IActionResult Form()
        {
            return View();
        }

    }
}
