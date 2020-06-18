using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class CabinetViewModel
    {
        public List<IngredDb> CabinetList { get; set; }
        public int UserId { get; set; }
        public List<string> AllIngredients { get; set; }
        public CabinetViewModel()
        {
            CabinetList = new List<IngredDb>();
        }
    }
}
