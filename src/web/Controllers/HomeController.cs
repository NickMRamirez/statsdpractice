using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("home/register/{level=starter}")]
        public IActionResult Register(string level) 
        {
            
            
            return View("~/Views/Home/register.cshtml");
        }
    }
}
