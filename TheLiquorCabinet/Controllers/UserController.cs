using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheLiquorCabinet.Models;

namespace TheLiquorCabinet.Controllers
{
    public class UserController : Controller
    {
        private readonly LiquorDBContext _db;

        public UserController()
        {
            _db = new LiquorDBContext();
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
            int id = _db.Users.Max(e => e.UserID);
            id++;
            User register = new User(name, id);
            _db.Users.Add(register);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
