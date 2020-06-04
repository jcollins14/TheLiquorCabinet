using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class IngredientController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await GetIngredient("1");
            return View();
        }

        public async Task<IActionResult> GetIngredient(string drinkId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("1/lookup.php?iid=" + drinkId);
            Ingredient result = new Ingredient(response);
            return View(result);
        }
    }
}
