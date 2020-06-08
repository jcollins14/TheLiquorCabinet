using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CocktailDbTools.Models;

namespace CocktailDbTools.Data
{
    public class CocktailDbContext : DbContext
    {
        public CocktailDbContext (DbContextOptions<CocktailDbContext> options)
            : base(options)
        {
        }

        public DbSet<CocktailDbTools.Models.DrinkDb> DrinkDb { get; set; }
    }
}
