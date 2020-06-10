using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DateConfirm(DateTime dateOfBirth)
        {
            var currentDate = DateTime.Now;
            TimeSpan age = currentDate - dateOfBirth;
            double years = age.TotalDays / 365.25;

            if (years < 21.00)
            {
                return RedirectToAction("Privacy");
            }
            else
            {
                return RedirectToAction("Index", "Drink");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}