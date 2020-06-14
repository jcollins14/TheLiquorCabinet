using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class DrinkIndexViewModel
    {
        public IngredientList Ingredients { get; set; }
        public List<DrinkDb> Drinks { get; set; }
    }
}
