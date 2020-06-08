using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocktailDbTools.Data;
using CocktailDbTools.Models;

namespace CocktailDbTools.Controllers
{
    public class DrinkDbsController : Controller
    {
        private readonly CocktailDbContext _context;

        public DrinkDbsController(CocktailDbContext context)
        {
            _context = context;
        }

        // GET: DrinkDbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrinkDb.ToListAsync());
        }

        // GET: DrinkDbs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkDb = await _context.DrinkDb
                .FirstOrDefaultAsync(m => m.idDrink == id);
            if (drinkDb == null)
            {
                return NotFound();
            }

            return View(drinkDb);
        }

        // GET: DrinkDbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrinkDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idDrink,strDrink,strDrinkAlternate,strTags,strVideo,strCategory,strIBA,strAlcoholic,strGlass,strInstructions,strDrinkThumb,strIngredient1,strIngredient2,strIngredient3,strIngredient4,strIngredient5,strIngredient6,strIngredient7,strIngredient8,strIngredient9,strIngredient10,strIngredient11,strIngredient12,strIngredient13,strIngredient14,strIngredient15,strMeasure1,strMeasure2,strMeasure3,strMeasure4,strMeasure5,strMeasure6,strMeasure7,strMeasure8,strMeasure9,strMeasure10,strMeasure11,strMeasure12,strMeasure13,strMeasure14,strMeasure15,dateModified")] DrinkDb drinkDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drinkDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinkDb);
        }

        // GET: DrinkDbs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkDb = await _context.DrinkDb.FindAsync(id);
            if (drinkDb == null)
            {
                return NotFound();
            }
            return View(drinkDb);
        }

        // POST: DrinkDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("idDrink,strDrink,strDrinkAlternate,strTags,strVideo,strCategory,strIBA,strAlcoholic,strGlass,strInstructions,strDrinkThumb,strIngredient1,strIngredient2,strIngredient3,strIngredient4,strIngredient5,strIngredient6,strIngredient7,strIngredient8,strIngredient9,strIngredient10,strIngredient11,strIngredient12,strIngredient13,strIngredient14,strIngredient15,strMeasure1,strMeasure2,strMeasure3,strMeasure4,strMeasure5,strMeasure6,strMeasure7,strMeasure8,strMeasure9,strMeasure10,strMeasure11,strMeasure12,strMeasure13,strMeasure14,strMeasure15,dateModified")] DrinkDb drinkDb)
        {
            if (id != drinkDb.idDrink)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drinkDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkDbExists(drinkDb.idDrink))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(drinkDb);
        }

        // GET: DrinkDbs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drinkDb = await _context.DrinkDb
                .FirstOrDefaultAsync(m => m.idDrink == id);
            if (drinkDb == null)
            {
                return NotFound();
            }

            return View(drinkDb);
        }

        // POST: DrinkDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var drinkDb = await _context.DrinkDb.FindAsync(id);
            _context.DrinkDb.Remove(drinkDb);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkDbExists(string id)
        {
            return _context.DrinkDb.Any(e => e.idDrink == id);
        }
    }
}
