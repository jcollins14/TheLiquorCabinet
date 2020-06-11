using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class HomeController : Controller
    {

        private readonly LiquorDBContext _db;
        private HttpClient _client;
        public string ApiKey = "api/json/v2/9973533";
        public HomeController()
        {
            _db = new LiquorDBContext();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://www.thecocktaildb.com/");
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
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
                return RedirectToAction("HomeNA");
            }
            else
            {
                return RedirectToAction("Home");
            }
        }
            public async Task<IActionResult> Home()
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
                //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
                var response = await client.GetStringAsync("1/random.php");
                Drink result = new Drink(response);

                return View(result);
            }

            public async Task<IActionResult> HomeNA()
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
                //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
                var response = await client.GetStringAsync("1/random.php?a=Non-Alcoholic");
                Drink result = new Drink(response);

                return View(result);

            }

            public IActionResult Privacy()
            {
                return View();
            }

            public IActionResult TestDBContext()
            {
                User testU = new User()
                {
                    Username = "John",
                    UserID = 4
                };
                Favorite testF = new Favorite()
                {
                    UserID = 2,
                    DrinkID = 11009
                };
                _db.Users.Add(testU);
                _db.Favorites.Add(testF);
                _db.SaveChanges();
                return View();
            }

            //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //public IActionResult Error()
            //{
            //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
    }