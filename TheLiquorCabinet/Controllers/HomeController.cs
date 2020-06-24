using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using RestSharp;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class HomeController : Controller
    {

        private readonly LiquorDBContext _context;
        private readonly HttpClient _client;
        private readonly string _apiKey = "api/json/v2/9973533";
        public HomeController()
        {
            _context = new LiquorDBContext();
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
        }

        public IActionResult Index()
        {
            ViewBag.Date = DateManiupulation(DateTime.Now);
            return View();
        }

        public string DateManiupulation(DateTime manip)
        {
            string[] split = manip.ToString("d").Split('/');
            if (split[0].Length == 1)
            {
                split[0] = split[0].Insert(0, "0");
            }
            if (split[1].Length == 1)
            {
                split[1] = split[1].Insert(0, "0");
            }
            string date = split[2] + '-' + split[0] + '-' + split[1];
            return date;
        }

        [HttpPost]
        public IActionResult DateConfirm(DateTime dateOfBirth)
        {
            var currentDate = DateTime.Now;
            TimeSpan age = currentDate - dateOfBirth;
            double years = age.TotalDays / 365.25;
            string birthday = DateManiupulation(dateOfBirth);
            HttpContext.Response.Cookies.Append("DoB", birthday);
            HttpContext.Response.Cookies.Append("Age", years.ToString());
            return RedirectToAction("Home");
        }
            
        public async Task<IActionResult> Home()
        {
            double.TryParse(HttpContext.Request.Cookies["Age"], out double age);
            if (age < 21)
            {
                return RedirectToAction("HomeNA");
            }
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/")
            };
        //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("1/random.php");
            Drink result = new Drink(response);
            HomeViewModel hvm = new HomeViewModel();
            hvm.IngredientList = await GetAllIngredients();
            hvm.Drink = result;
            hvm.DrinksIndex = _context.DrinkDb.ToList();
            ViewBag.Username = HttpContext.Request.Cookies["User"];
            return View(hvm);
        }

        //Returns a random drink from thecocktaildb.com
        public async Task<IActionResult> FeelingLucky()
        {
            var response = await _client.GetStringAsync(_apiKey + "/random.php");
            Drink result = new Drink(response);
            return RedirectToAction("GetDrink", "Drink", result);
        }
        

        public async Task<IActionResult> HomeNA()
        {
            
            Drink result = await GetRandomNADrink();           
            HomeViewModel hvm = new HomeViewModel();
            hvm.IngredientList = await GetAllIngredients();
            hvm.Drink = result;
            hvm.DrinksNA = await DrinkFilterByNA();
            hvm.DrinksIndex = _context.DrinkDb.ToList();
            hvm.DbIngreds = _context.IngredDb.ToList();
            ViewBag.Username = HttpContext.Request.Cookies["User"];
            return View(hvm);
        }
      
        public async Task<DrinkListSearch> DrinkFilterByNA()
        {
           
            DrinkListSearch searchResult = new DrinkListSearch(await _client.GetStringAsync(_apiKey + "/filter.php?a=Non_Alcoholic"));

            return searchResult;

        }

        //Returns a random non-alcoholic drink from thecocktaildb.com
        public async Task<Drink> GetRandomNADrink()
        {
            DrinkListSearch searchResult = new DrinkListSearch(await _client.GetStringAsync(_apiKey + "/filter.php?a=Non_Alcoholic"));
            Random rng = new Random();
            string id = searchResult.IdList[rng.Next(0, searchResult.IdList.Count)];
            Drink result = new Drink(await _client.GetStringAsync(_apiKey + "/lookup.php?i=" + id));
            return result;
        }


        public async Task<IActionResult> FeelingLuckyNA()
        {
            DrinkListSearch searchResult = new DrinkListSearch(await _client.GetStringAsync(_apiKey + "/filter.php?a=Non_Alcoholic"));
            Random rng = new Random();
            string id = searchResult.IdList[rng.Next(0, searchResult.IdList.Count)];
            Drink result = new Drink(await _client.GetStringAsync(_apiKey + "/lookup.php?i=" + id));

            return RedirectToAction("GetDrink", "Drink", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IngredientList> GetAllIngredients()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/")
            };
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            return result;
        }

        //debug method
        //public IActionResult TestDBContext()
        //{
        //    User testU = new User()
        //    {
        //        Username = "John",
        //        UserID = 4
        //    };
        //    Favorite testF = new Favorite()
        //    {
        //        UserID = 2,
        //        DrinkID = 11009
        //    };
        //    _context.Users.Add(testU);
        //    _context.Favorites.Add(testF);
        //    _context.SaveChanges();
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }

    
}