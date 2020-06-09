using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CocktailDbTools.Data;
using CocktailDbTools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class HomeController : Controller
    {
        private readonly LiquorDBContext _db;
        private readonly ILogger<HomeController> _logger;
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
        public async Task<IActionResult> UpdateDb()
        {
            string[] categories = new string[] { "Alcoholic", "Non_Alcoholic", "Optional_Alcohol" };
            DrinkListSearch searchResult;
            foreach (string category in categories)
            {
                searchResult = new DrinkListSearch(await _client.GetStringAsync(ApiKey + "/filter.php?a=" + category));
                foreach (var id in searchResult.IdList)
                {
                    if (!_db.DrinkDb.Any(e => e.idDrink == id))
                    {
                        DrinkResponse response = new DrinkResponse(await _client.GetStringAsync(ApiKey + "/lookup.php?i=" + id));
                        _db.Add(response.ResponseDrink);
                        _db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
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