using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLiquorCabinet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace TheLiquorCabinet.Controllers
{
    public class ScraperController : Controller
    {
            private HttpClient _client;
            private readonly LiquorDBContext _context;
            private readonly string _apiKey = "api/json/v2/9973533";
            public ScraperController(LiquorDBContext context)
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri("https://www.thecocktaildb.com/");
                _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
                _context = context;
            }
            public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpdateDb()
        {
            string[] categories = new string[] { "Alcoholic", "Non_Alcoholic", "Optional_Alcohol" };
            DrinkListSearch searchResult = new DrinkListSearch();
            foreach (string category in categories)
            {
                searchResult = new DrinkListSearch(await _client.GetStringAsync(_apiKey + "/filter.php?a=" + category));
                foreach (var id in searchResult.IdList)
                {
                    if (!_context.DrinkDb.Any(e => e.IdDrink == id))
                    {
                        DrinkResponse response = new DrinkResponse(await _client.GetStringAsync(_apiKey + "/lookup.php?i=" + id));
                        _context.Add(response.ResponseDrink);
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
