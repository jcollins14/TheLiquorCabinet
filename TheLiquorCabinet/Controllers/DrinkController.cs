using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class DrinkController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await GetDrink(11007);
            return View();
        }
        string apiKey = "9973533";
        public async Task<IActionResult> GetDrink(int drinkId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync(apiKey + "/lookup.php?i=" + drinkId);
            Drink result = new Drink(response);
            return View(result);
        }
    }
}
