using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class DrinkListSearch
    {
        public List<string> IdList { get; set; }

        public DrinkListSearch()
        {
            this.IdList = new List<string>();
        }

        public DrinkListSearch(string ApiText)
        {
            this.IdList = new List<string>();
            JObject parse = JObject.Parse(ApiText);
            foreach (var item in parse["drinks"])
            {
                this.IdList.Add(item["idDrink"].ToString());
            }
        }

    }
}
