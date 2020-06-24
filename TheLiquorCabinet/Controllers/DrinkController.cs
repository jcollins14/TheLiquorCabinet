using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class DrinkController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "api/json/v2/9973533";
        private readonly LiquorDBContext _context;
        public DrinkController(LiquorDBContext context)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            _context = context;
        }
        //Index passes list of ingredients to the view for use in the select2 search bar.
        public async Task<IActionResult> Index()
        {
            DrinkIndexViewModel model = new DrinkIndexViewModel
            {
                Ingredients = await GetAllIngredients(),
                Drinks = _context.DrinkDb.ToList()
            };
            return View(model);
        }

        //Select2 powered filter stores selection as an array which is passed from index view.
        public IActionResult DrinkListView(List<Drink> drinks)
        {
            
            return View("DrinkListView", drinks);
        }
        public IActionResult CabinetDrinkListView(List<Drink> drinks)
        {

            return View("DrinkListView", drinks);
        }
        public async Task<IActionResult> DrinksByCabinet()
        {
            string[] ingredients = (string[])TempData["Cabinet"];
            CabinetSearchViewModel drinks = await GetDrinksByCabinet(ingredients.ToList());
            return View("CabinetDrinkListView", drinks);
        }
        public async Task<List<Drink>> GetDrinks(List<string> search)
        {
            List<Drink> drinks = new List<Drink>();
            foreach (string drinkName in search)
            {
                string manipulate = drinkName;
                manipulate = manipulate.Trim().ToLower().Replace(' ','_');
                var response = await _client.GetStringAsync(_apiKey + "/search.php?s=" + manipulate);
                Drink result = new Drink(response);
                drinks.Add(result);
            }
            return drinks;
        }

        public async Task<IActionResult> GetDrink(int ID)
        {
            var response = await _client.GetStringAsync(_apiKey + "/lookup.php?i=" + ID);
            Drink result = new Drink(response);
            for (int i = 0; i < result.Ingredients.Count; i++)
            {
                IngredientResponse ingredient = new IngredientResponse(await _client.GetStringAsync(_apiKey + "/search.php?i=" + result.Ingredients[i]));
                int.TryParse(HttpContext.Request.Cookies["UserID"], out int userID);
                if (userID == 0)
                {
                    result.IngredAvail.Add(false);
                }
                else
                {
                  IngredOnHand check = _context.Cabinet.Where(x => x.UserID == userID && x.IngredID == ingredient.ResponseIngred.Id).FirstOrDefault();
                    if (check != null)
                    {
                        result.IngredAvail.Add(true);
                    }
                    else
                    {
                        result.IngredAvail.Add(false);
                    }
                }  
            }
            ViewBag.loggedIn = HttpContext.Request.Cookies["User"];
            return View(result);
        }
        public async Task<IActionResult> GetDrinkByName(string name)
        {
            if (name.Contains('&'))
            {
                name = name.Replace("&", "%26");
            }
            var response = await _client.GetStringAsync(_apiKey + "/search.php?s=" + name.Trim().ToLower().Replace(' ', '_'));
            Drink result = new Drink(response);
            for (int i = 0; i < result.Ingredients.Count; i++)
            {
                IngredientResponse ingredient = new IngredientResponse(await _client.GetStringAsync(_apiKey + "/search.php?i=" + result.Ingredients[i]));
                int.TryParse(HttpContext.Request.Cookies["UserID"], out int userID);
                if (userID == 0)
                {
                    result.IngredAvail.Add(false);
                }
                else
                {
                    IngredOnHand check = _context.Cabinet.Where(x => x.UserID == userID && x.IngredID == ingredient.ResponseIngred.Id).FirstOrDefault();
                    if (check != null)
                    {
                        result.IngredAvail.Add(true);
                    }
                    else
                    {
                        result.IngredAvail.Add(false);
                    }
                }
            }
            ViewBag.loggedIn = HttpContext.Request.Cookies["User"];
            return View("GetDrink", result);
        }
        public async Task<IActionResult> DrinkNameSearch(string[] names)
        {
            if (names.Length == 1)
            {
                return await GetDrinkByName(names[0]);
            }
            return DrinkListView( await GetDrinks(names.ToList()));
        }
        public async Task<IActionResult> SearchMultipleIngredients(List<string> ingredients)
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

            var response = await _client.GetStringAsync(_apiKey + "/filter.php?i=" + endpoint);
            JObject parse = JObject.Parse(response);
            List<string> result = new List<string>();
            for (int i = 0; i < parse["drinks"].Count(); i++)
            {
                 string drinkName = (string)parse["drinks"][i]["strDrink"];
                result.Add(drinkName);
            }
            string joined = String.Join(", ", ingredients);
            ViewBag.IngredientNames = joined;
            List<Drink> drinks = await GetDrinks(result);
            return DrinkListView(drinks);
        }
        public async Task<IActionResult> SearchMultipleIngredientsNA(List<string> ingredients)
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

            var response = await _client.GetStringAsync(_apiKey + "/filter.php?i=" + endpoint);
            JObject parse = JObject.Parse(response);
            List<string> result = new List<string>();
            for (int i = 0; i < parse["drinks"].Count(); i++)
            {
                string drinkName = (string)parse["drinks"][i]["strDrink"];
                result.Add(drinkName);
            }
            string joined = String.Join(", ", ingredients);
            ViewBag.IngredientNames = joined;
            List<Drink> drinks = await GetDrinks(result);
            List<Drink> naDrinks = drinks.Where(e => e.IsAlcoholic == false).ToList();
            return DrinkListView(naDrinks);
        }

        public async Task<CabinetSearchViewModel> GetDrinksByCabinet(List<string> ings)
        {
            ings = ings.ConvertAll(e => e.ToLower());
            ings = ParseGenerics(ings);
            CabinetSearchViewModel result = new CabinetSearchViewModel();
            List<string> canMake = new List<string>();
            List<string> missingOne = new List<string>();
            foreach (DrinkDb drink in 
                //commented below is test code for manually providing dring to method.
                //new List<DrinkDb>() { new DrinkDb() { IdDrink = "11011", StrIngredient1 = "Vodka", StrIngredient2 = "Lime Juice", StrIngredient3 = "Ginger Ale", StrDrink = "Moscow Mule"} }
                _context.DrinkDb.ToList()
                )
            {
                List<string> drinkIngs = drink.GetDrinkDbIngredients();
                int numberOfMissingIngredients = CabinetContainsDrink(ings, drinkIngs);
                if (numberOfMissingIngredients == 0)
                {
                    canMake.Add(drink.StrDrink);
                }
                else if(numberOfMissingIngredients == 1)
                {
                    missingOne.Add(drink.StrDrink);
                }
            }
            result.CanMake = await GetDrinks(canMake);
            result.MissingOne = await GetDrinks(missingOne);
            return result;
        }
        public int CabinetContainsDrink(List<string> cabinet, List<string> drinkIngs)
        {
            //this code is injecting the designated basic ingredients from our database into the cabinet during the search.
            //we can comment it out when we include these in the user's cabinet when it's generated.
            //List<string> basics = _context.IngredDb.Where(e => e.Type == "Basic").Select(e => e.Name.ToLower()).ToList();
            //foreach (var item in basics)
            //{
            //    cabinet.Add(item);
            //}

            //cabinet = ParseGenerics(cabinet);
            int check = drinkIngs.Except(cabinet).Count();
            return check;
        }
        public List<string> ParseGenerics(List<string> cabinet)
        {
            if (cabinet.Contains("whisky") && !cabinet.Contains("whiskey"))
            {
                cabinet.Add("whiskey");
            }

            List<string> generics = new List<string>() { "Vodka", "Gin", "Rum", "Whiskey", "Brandy", "Tequila"};
            foreach (var generic in generics)
            {
                if (cabinet.Contains(generic.ToLower()))
                {
                    cabinet.AddRange(_context.IngredDb.Where(e => e.Type == generic).Select(e => e.Name.ToLower()).ToList());
                }
            }
            return cabinet;
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
        public async Task<Ingredient> IngredientInfo(string Name)
        {
            Ingredient result = await GetIngredient(Name);
            return result;
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
    }


}
