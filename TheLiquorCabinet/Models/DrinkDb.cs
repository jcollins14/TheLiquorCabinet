using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{

    public class DrinkDb
    {
        [Key]
        public string IdDrink { get; set; }
        public string StrDrink { get; set; }
        public string StrIngredient1 { get; set; }
        public string StrIngredient2 { get; set; }
        public string StrIngredient3 { get; set; }
        public string StrIngredient4 { get; set; }
        public string StrIngredient5 { get; set; }
        public string StrIngredient6 { get; set; }
        public string StrIngredient7 { get; set; }
        public string StrIngredient8 { get; set; }
        public string StrIngredient9 { get; set; }
        public string StrIngredient10 { get; set; }
        public string StrIngredient11 { get; set; }
        public string StrIngredient12 { get; set; }
        public string StrIngredient13 { get; set; }
        public string StrIngredient14 { get; set; }
        public string StrIngredient15 { get; set; }


        public List<string> GetDrinkDbIngredients()
        {
            List<string> ings = new List<string>(){
            this.StrIngredient1, this.StrIngredient2, this.StrIngredient3,
            this.StrIngredient4, this.StrIngredient5, this.StrIngredient6,
            this.StrIngredient7, this.StrIngredient8, this.StrIngredient9,
            this.StrIngredient10, this.StrIngredient11, this.StrIngredient12,
            this.StrIngredient13, this.StrIngredient14, this.StrIngredient15
        };
            int firstEmpty = ings.IndexOf(null);
            ings.RemoveRange(firstEmpty, ings.Count() - firstEmpty);
            ings = ings.ConvertAll(e => e.ToLower());
            return ings;
        }
    }
}
