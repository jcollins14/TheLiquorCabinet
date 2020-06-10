﻿using System;
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

        IngredientList resultlist = new IngredientList();
        public async Task<IActionResult> Index()

        {
<<<<<<< HEAD
            await GetIngredient("9973533");
            return View();
=======
            //await GetIngredient("1");
            return RedirectToAction("GetAllIngredients");
>>>>>>> dev
        }

        public async Task<IActionResult> GetIngredient(string ingredientId)
        {
            var client = new HttpClient();
<<<<<<< HEAD
            client.BaseAddress = new Uri("https://www.themealdb.com/api/json/v2/9973533/latest.php");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list" + ingredientId);
            Ingredient result = new Ingredient(response);
            return View(result);
        }

        public async Task<IActionResult> GetAllIngredients()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.themealdb.com/api/json/v2/9973533/latest.php");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            //IngredientList result = new IngredientList(response);
            return RedirectToAction("SearchByIngredient");
        }

        public IActionResult SearchByIngredient()
        {

            return View();
=======
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/lookup.php?iid=" + ingredientId);
            Ingredient result = new Ingredient(response);
            return View(result);
        }
        public async Task<IActionResult> GetAllIngredients()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList (response);
            resultlist = result;
            return RedirectToAction("SearchByIngredient");
        }
        public async Task<IngredientList> GetAll()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v2/");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            var response = await client.GetStringAsync("9973533/list.php?i=list");
            IngredientList result = new IngredientList(response);
            resultlist = result;
            return result;
        }

        public async Task<IActionResult> SearchByIngredient()
        {
            IngredientList response = await GetAll();
            return View(response);
>>>>>>> dev
        }
    }
}
