using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{

    public class UserController : Controller
    {
        private readonly LiquorDBContext _context;
        private readonly string _apiKey = "api/json/v2/9973533";
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            _context = new LiquorDBContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        //Send username to Azure along with DOB from cookie
        [HttpPost]
        public IActionResult Register(string name, DateTime dateOfBirth)
        {

            //This took way longer than need be
            var registerUser = new User()
            {
                Username = name,
                Birthday = dateOfBirth //make sure this name matches the .cshtml input name="[name]" as well!
            };

            _context.Users.Add(registerUser);
            _context.SaveChanges();
            int userID = _context.Users.FirstOrDefault(n => n.Username == name).UserID;
            HttpContext.Response.Cookies.Append("UserID", userID.ToString());
            TimeSpan age = DateTime.Today - dateOfBirth;
            double years = age.TotalDays / 365.25;
            HttpContext.Response.Cookies.Append("Age", years.ToString());
            
            if(years < 21)
            {
                return RedirectToAction("HomeNA", "Home");
            }

            return RedirectToAction("Home", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginUser(string name)
        {
            var user = _context.Users.Where(x => x.Username == name).FirstOrDefault();
            if (user is object)
            {
                TimeSpan age = DateTime.Today - user.Birthday;
                double years = age.TotalDays / 365.25;

                if (years < 21) //Age check validation
                {
                    return RedirectToAction("HomeNA", "Home");
                }

                return RedirectToAction("Home", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //Pull all ingredients from API and put into list for Select2
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

        public async Task<IActionResult> Cabinet()
        {

            int UserID = int.Parse(HttpContext.Request.Cookies["UserID"]);
            CabinetViewModel cabinetModel = new CabinetViewModel();
            if (UserID != 0)
            {
                List<IngredOnHand> savedCabinet = _context.Cabinet.Where(e => e.UserID == UserID).ToList();
                foreach (IngredOnHand item in savedCabinet)
                {
                    Ingredient response = new Ingredient(await _client.GetStringAsync(_apiKey + "/lookup.php?iid=" + item.IngredID));
                    IngredOnHand result = new IngredOnHand()
                    {
                        UserID = UserID,
                        IngredID = response.ID
                    };
                    cabinetModel.CabinetList.Add(result);
                }
            }
            return View("Cabinet", cabinetModel);
        }
        public async Task<IActionResult> AddToCabinet(List<string> ingredients)
        {
            var UserID = HttpContext.Request.Cookies["UserID"];
            List<Ingredient> cabinetUpload = new List<Ingredient>();
            foreach (string ingredient in ingredients)
            {
                var response = await _client.GetStringAsync(_apiKey + "/search.php?i=" + ingredient);
                Ingredient result = new Ingredient(response);
                cabinetUpload.Add(result);
            }
            foreach (Ingredient item in cabinetUpload)
            {
                IngredOnHand upload = new IngredOnHand()
                {
                    UserID = int.Parse(UserID),
                    IngredID = item.ID
                };
                _context.Cabinet.Add(upload);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Drink");
        }

        public async void AddDefaultIngredients()
        {
            List<string> defaults = new List<string>()
            {
                "Black Pepper",
                "Brown Sugar",
                "Butter",
                "Cayenne Pepper",
                "Cinnamon",
                "Cola",
                "Cold Water",
                "Egg White",
                "Egg Yolk",
                "Egg",
                "Honey",
                "Ice",
                "Jelly",
                "Milk",
                "Nutmeg",
                "Pepper",
                "Plain Flour",
                "Salt",
                "Soy Sauce",
                "Sugar",
                "Sugar Syrup",
                "Water"
            };
            await AddToCabinet(defaults);
        }
    }
}
