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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IngredientInfo(string ingredientId)
        {
            Ingredient result = await GetIngredient(ingredientId);
            return View(result);
        }
        public async Task<Ingredient> GetIngredient(string ingredientId)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.themealdb.com/api/json/v2/9973533/latest.php")
            };
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list" + ingredientId);
            Ingredient result = new Ingredient(response);
            return result;
        }

        public async Task<IngredientList> GetAllIngredients()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.themealdb.com/api/json/v2/9973533/latest.php")
            };
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            return result;
        }
    }
}
