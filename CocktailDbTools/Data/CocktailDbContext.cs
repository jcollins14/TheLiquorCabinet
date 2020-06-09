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
        public CocktailDbContext()
        {

        }
        public CocktailDbContext (DbContextOptions<CocktailDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-LHOG1J7\\SQLEXPRESS;Database=CocktailDB;Trusted_Connection=True;");
            }
        }
        public virtual DbSet<CocktailDbTools.Models.DrinkDb> DrinkDb { get; set; }
    }
}
