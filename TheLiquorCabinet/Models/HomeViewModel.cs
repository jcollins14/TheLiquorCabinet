﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class HomeViewModel
    {
        public Drink Drink { get; set; }
        public DrinkListSearch DrinksNA { get; set; }
        public IngredientList IngredientList { get; set; }
        public List<DrinkDb> DrinksIndex { get; set; }
        public List<IngredDb> DbIngreds { get; set; }
    }
}
