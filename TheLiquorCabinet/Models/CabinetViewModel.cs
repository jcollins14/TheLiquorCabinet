using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class CabinetViewModel
    {
        public IngredientList IngredList { get; set; }
        public List<IngredOnHand> CabinetList { get; set; }
    }
}
