﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLiquorCabinet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.Internal;

namespace TheLiquorCabinet.Controllers
{
    public class ScraperController : Controller
    {
            private readonly HttpClient _client;
            private readonly LiquorDBContext _context;
            private readonly string _apiKey = "api/json/v2/9973533";
            public ScraperController(LiquorDBContext context)
            {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://www.thecocktaildb.com/")
            };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
                _context = context;
            }

        public async Task<IActionResult> UpdateDb()
        {
            string[] categories = new string[] { "Alcoholic", "Non_Alcoholic", "Optional_Alcohol" };
            DrinkListSearch searchResult = new DrinkListSearch();
            List<DrinkDb> duplicate = new List<DrinkDb>();
            //pull all drinks from thecocktaildb.com and check for duplicates in azure table
            foreach (string category in categories)
            {
                searchResult = new DrinkListSearch(await _client.GetStringAsync(_apiKey + "/filter.php?a=" + category));
                foreach (var id in searchResult.IdList)
                {
                    if (!_context.DrinkDb.Any(e => e.IdDrink.ToString().Contains(id)))
                    {
                        DrinkResponse response = new DrinkResponse(await _client.GetStringAsync(_apiKey + "/lookup.php?i=" + id));
                        duplicate.Add(response.ResponseDrink);
                    }
                }
            }
            //add to azure sql database
            foreach (DrinkDb recipe in duplicate)
            {
                _context.DrinkDb.Add(recipe);
            }
            _context.SaveChanges();
            return RedirectToAction("Home", "Home");
        }
        public async Task<IActionResult> UpdateIngredDb()
        {
            IngredientList ingredients = await GetAllIngredients();
            List<IngredDb> duplicate = new List<IngredDb>();
            //pull all drinks from thecocktaildb.com and check for duplicates in azure table
            foreach (var name in ingredients.IngredientNames)
            {
                if (!_context.IngredDb.Any(e => e.Name.Contains(name)))
                {
                    IngredientResponse response = new IngredientResponse(await _client.GetStringAsync(_apiKey + "/search.php?i=" + name));
                    duplicate.Add(response.ResponseIngred);
                }
            }
            //add to azure sql database
            foreach (IngredDb ingred in duplicate)
            {
                _context.IngredDb.Add(ingred);
            }
            _context.SaveChanges();
            return RedirectToAction("Home", "Home");
        }
        //this method reads the IngredienTypes/Basics.txt file, sets all matching ingredients to basic in database
        //then it removes the basic type from any in the database that don't appear in the file
        public IActionResult SetBasicTypes()
        {
            List<string> basics = new List<string>();
            using (StreamReader inputFile = new StreamReader("IngredientTypes/Basics.txt"))
            {
                while (!inputFile.EndOfStream)
                {
                    string ingred;
                    if (!string.IsNullOrEmpty((ingred = inputFile.ReadLine())))
                    {
                        basics.Add(ingred);
                    }
                }
            }
            foreach (var basic in basics)
            {
                _context.IngredDb.FirstOrDefault(e => e.Name == basic).Type = "Basic";
            }
            foreach (var ingred in _context.IngredDb)
            {
                if (ingred.Type == "Basic" && !basics.Contains(ingred.Name))
                {
                    ingred.Type = null;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Home", "Home");
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
    }
}
