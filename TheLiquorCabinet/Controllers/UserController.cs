using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
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
            string DoB = HttpContext.Request.Cookies["DoB"];
            if (DoB == null)
            {
                ViewBag.Date = DateManiupulation(DateTime.Now);
            }
            else
            {
                ViewBag.Date = DoB;
            }
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

        //Send username to Azure along with DOB from cookie
        [HttpPost]
        public async Task<IActionResult> Register(string name, DateTime dateOfBirth)
        {
            var registerUser = new User()
            {
                Username = name,
                Birthday = dateOfBirth //make sure this name matches the .cshtml input name="[name]" as well!
            };

            if (_context.Users.Where(x => x.Username == name).FirstOrDefault() != null)
            {
                return RedirectToAction("RegisterError");
            }
            if(name == null)
            {
                return RedirectToAction("Register");
            }
            else
            {
                _context.Users.Add(registerUser);
                _context.SaveChanges();
            }
            int userID = _context.Users.FirstOrDefault(n => n.Username == name).UserID;
            HttpContext.Response.Cookies.Append("UserID", userID.ToString());
            TimeSpan age = DateTime.Today - dateOfBirth;
            double years = age.TotalDays / 365.25;
            HttpContext.Response.Cookies.Append("Age", years.ToString());
            HttpContext.Response.Cookies.Append("User", name);
            List<string> defaults = GetDefaultIngredients();
            await AddToCabinet(defaults, userID);
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
            if (user is null)
            {
                return RedirectToAction("LoginError");
            }
            int userID = _context.Users.FirstOrDefault(n => n.Username == name).UserID;
            if (user is object)
            {
                TimeSpan age = DateTime.Today - user.Birthday;
                double years = age.TotalDays / 365.25;
                HttpContext.Response.Cookies.Append("UserID",user.UserID.ToString());
                HttpContext.Response.Cookies.Append("Age", years.ToString());
                HttpContext.Response.Cookies.Append("User", user.Username);
                if (years < 21) //Age check validation
                {
                    return RedirectToAction("HomeNA", "Home");
                }

                return RedirectToAction("Home", "Home");
            }
            else
            {
                return RedirectToAction("LoginError");
            }
        }

        public IActionResult LoginError()
        {
            return View();
        }

        //Pull all ingredients from API and put into list for Select2
        public async Task<IngredientList> GetAllIngredients()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/")
            };
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            return result;
        }

        public IActionResult Cabinet()
        {
            string Username = HttpContext.Request.Cookies["User"];
            if (Username != null)
            {
                ViewBag.Username = Username;
            }
            int UserID;
            if (HttpContext.Request.Cookies["UserID"] != null)
            {
                UserID = int.Parse(HttpContext.Request.Cookies["UserID"]);
                
            }
            else
            {
                CabinetViewModel cabinetViewModel = new CabinetViewModel() { UserId = 0};
                return View("Cabinet", cabinetViewModel);
            }
            CabinetViewModel cabinetModel = new CabinetViewModel();
            if (UserID != 0)
            {
                List<int> savedCabinetIds = _context.Cabinet.Where(e => e.UserID == UserID).Select(e => e.IngredID).ToList();
                List<IngredDb> savedCabinet = new List<IngredDb>();
                foreach (var id in savedCabinetIds)
                {
                    cabinetModel.CabinetList.Add(_context.IngredDb.FirstOrDefault(e => e.Id == id));
                }
            }
            cabinetModel.CabinetList = cabinetModel.CabinetList.OrderBy(e => e.Name).ToList();
            //code below takes id list of ingredients in database and filters out those already in the users cabinet.
            double.TryParse(HttpContext.Request.Cookies["Age"], out double age);
            
            var allIng = _context.IngredDb.Select(e => e.Id).ToList();
            var notInCabinet = allIng.Except(cabinetModel.CabinetList.Select(e => e.Id)).ToList();
            if (age > 21 )
            {
                cabinetModel.AllIngredients = _context.IngredDb.Where(e => notInCabinet.Contains(e.Id)).Select(e => e.Name).ToList();
            }
            else
            {
                cabinetModel.AllIngredients = _context.IngredDb.Where(e => notInCabinet.Contains(e.Id) && e.Alcohol != "Yes").Select(e => e.Name).ToList();
            }
            cabinetModel.AllIngredients.Sort();
            TempData.Clear();
            TempData.Add("Cabinet", cabinetModel.CabinetList.Select(e => e.Name).ToList());
            cabinetModel.UserId = int.Parse(HttpContext.Request.Cookies["UserID"]);
            ViewBag.Username = HttpContext.Request.Cookies["User"];
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
        //overloaded method for use in the Register Method - async was causing issues with cookie generation
        public async Task<IActionResult> AddToCabinet(List<string> ingredients, int userID)
        {
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
                    UserID = userID,
                    IngredID = item.ID
                };
                _context.Cabinet.Add(upload);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Drink");
        }

        public List<string> GetDefaultIngredients()
        {
            List<string> defaults = new List<string>();
            List<IngredDb> response = _context.IngredDb.Where(x => x.Type == "Basic").ToList();
            foreach (IngredDb iterate in response)
            {
                string name = iterate.Name;
                defaults.Add(name);
            }
            return defaults;
        }
        public IActionResult LogOut()
        {
            List<string> cookies = new List<string>() { "Age", "DoB", "UserID", "User" };
            foreach (string cookieName in cookies)
            {
                HttpContext.Response.Cookies.Delete(cookieName);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterError()
        {
            return View();
        }
    }
}
