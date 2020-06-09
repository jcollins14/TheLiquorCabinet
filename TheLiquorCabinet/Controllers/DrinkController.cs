using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class DrinkController : Controller
    {
        private HttpClient _client;
        public string ApiKey = "api/json/v2/9973533";
        private readonly LiquorDBContext _context;
        public DrinkController(LiquorDBContext context)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://www.thecocktaildb.com/");
           _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            _context = context;
        }
        //Index passes list of ingredients to the view for use in the select2 search bar.
        public async Task<IActionResult> Index()
        {
            return View(await GetAllIngredients());
        }

        //Select2 powered filter stores selection as an array which is passed from index view.
        public async Task<IActionResult> DrinkListView(string[] ingredients)
        {
            List<string> names = await SearchMultipleIngredients(ingredients.ToList());
            List<Drink> drinks = await GetDrinks(names);
            return View(drinks);
        }

        public async Task<List<Drink>> GetDrinks(List<string> search)
        {
            List<Drink> drinks = new List<Drink>();
            foreach (string drinkName in search)
            {
                string manipulate = drinkName;
                manipulate = manipulate.Trim().ToLower().Replace(' ','_');
                var response = await _client.GetStringAsync(ApiKey + "/search.php?s=" + manipulate);
                Drink result = new Drink(response);
                drinks.Add(result);
            }
            return drinks;
        }

        public async Task<IActionResult> GetDrink(int drinkId)
        {
            var response = await _client.GetStringAsync("/lookup.php?i=" + drinkId);
            Drink result = new Drink(response);
            return View(result);
        }

        public async Task<List<string>> SearchMultipleIngredients(List<string> ingredients)
        {
            string endpoint = "";
            for (int i = 0; i < ingredients.Count; i++)
            {
                string add = ingredients[i].Trim();
                add = add.Replace(' ', '_');
                if (i != ingredients.Count - 1)
                {
                add += ',';
                }
                endpoint += add;
            }

            var response = await _client.GetStringAsync(ApiKey + "/filter.php?i=" + endpoint);
            JObject parse = JObject.Parse(response);
            List<string> result = new List<string>();
            for (int i = 0; i < parse["drinks"].Count(); i++)
            {
                string drinkName = (string)parse["drinks"][i]["strDrink"];
                result.Add(drinkName);
            }
            return result;
        }

        public async Task<List<string>> GetDrinksByCabinet(List<string> ings)
        {
            List<Drink> result = new List<Drink>();
            
        }
        public async Task<IngredientList> GetAllIngredients()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            return result;
        }
    }
}
