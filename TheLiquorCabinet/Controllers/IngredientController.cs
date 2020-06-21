using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using TheLiquorCabinet.Models;
using Newtonsoft.Json.Linq;

namespace TheLiquorCabinet.Controllers
{
    public class IngredientController : Controller
    {
        private readonly string _apiKey = "api/json/v2/9973533";
        private readonly LiquorDBContext _context;
        public IngredientController(LiquorDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IngredientInfo(string Name)
        {
            Ingredient result = await GetIngredient(Name);
            return View(result);
        }
        public async Task<Ingredient> GetIngredient(string Name)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/")
            };
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/search.php?i=" + Name);
            Ingredient result = new Ingredient(response);
            return result;
        }

        public async Task<IngredientList> GetAllIngredients()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktail.com/api/json/v2/9973533/latest.php")
            };
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            return result;
        }

        public async Task<IActionResult> RegisterIngredientsToUser(List<string> ingreds)
        {
            List<IngredOnHand> cabinet = new List<IngredOnHand>();
            int UserID = int.Parse(HttpContext.Request.Cookies["UserID"]);
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            foreach (string name in ingreds)
            {
                var response = await client.GetStringAsync(_apiKey + "/search.php?i=" + name);
                IngredOnHand ingredient = new IngredOnHand(response, UserID);
                cabinet.Add(ingredient);
            }
            foreach (IngredOnHand save in cabinet)
            {
                _context.Cabinet.Add(save);
                //if (_context.Cabinet.Contains(save))
                //{
                //    _context.Cabinet.Add(save);
                //}
            }
            _context.SaveChanges();
            return RedirectToAction("Cabinet", "User");
        }
        public async Task<IActionResult> RemoveIngredientsFromUser(List<string> ingreds)
        {
            List<IngredOnHand> cabinet = new List<IngredOnHand>();
            int UserID = int.Parse(HttpContext.Request.Cookies["UserID"]);
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            foreach (string name in ingreds)
            {
                var response = await client.GetStringAsync(_apiKey + "/search.php?i=" + name);
                IngredOnHand ingredient = new IngredOnHand(response, UserID);
                cabinet.Add(ingredient);
            }
            foreach (IngredOnHand save in cabinet)
            {
                if (_context.Cabinet.Contains(save))
                {
                    _context.Cabinet.Remove(save);
                }
            }
            _context.SaveChanges();
            return RedirectToAction("CabinetView", "User");
        }
        public async Task<IActionResult> RemoveOneIngredient(string ingred)
        {
            int UserID = int.Parse(HttpContext.Request.Cookies["UserID"]);
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            var response = await client.GetStringAsync(_apiKey + "/search.php?i=" + ingred);
            IngredOnHand ingredient = new IngredOnHand(response, UserID);
            
            IngredOnHand remove = _context.Cabinet.Where(e => e.UserID == ingredient.UserID).FirstOrDefault(e => e.IngredID == ingredient.IngredID);
            if (remove != null)
            {
                _context.Remove(remove);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Cabinet", "User");
        }


    }
}
