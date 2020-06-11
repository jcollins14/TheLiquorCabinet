using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Loader;
using System.Threading.Tasks;
using AspNetCore;
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

        [HttpPost]
        public IActionResult Register(string name)
        {
            User register = new User(name);
            _context.Users.Add(register);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Cabinet()
        {
            List<Ingredient> cabinet = new List<Ingredient>();
            if (SavedCookie.UserID != null)
            {
                List<IngredOnHand> savedCabinet = _context.Cabinet.Where(e => e.UserID == SavedCookie.UserID).ToList;
                foreach (IngredOnHand item in savedCabinet)
                {
                    var response = await _client.GetStringAsync(_apiKey + "/list.php?i=list" + item.IngredID);
                    Ingredient result = new Ingredient(response);
                    cabinet.Add(result);
                }
            }
            return View(cabinet);
        }

        public async Task<IActionResult> AddToCabinet(List<string> ingredients)
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
                    UserID = SavedCookie.UserID,
                    IngredID = item.ID
                };
                _context.Cabinet.Add(upload);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Drink");
        }
    }
}
