using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class CabinetSearchViewModel
    {
        public List<Drink> CanMake { get; set; }
        public List<Drink> MissingOne { get; set; }
        public CabinetSearchViewModel()
        {
            CanMake = new List<Drink>();
            MissingOne = new List<Drink>();
        }
    }
}
