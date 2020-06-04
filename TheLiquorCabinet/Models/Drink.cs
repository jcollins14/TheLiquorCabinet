using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheLiquorCabinet.Models
{
    public class Drink
    {
        [JsonProperty("idDrink")]
        public int ID { get; set; }
        [JsonProperty("strDrink")]
        public string Name { get; set; }
        public object StrTags { get; set; }
        public object StrVideo { get; set; }
        public string StrCategory { get; set; }
        public object StrIBA { get; set; }
        [JsonProperty("strAlcoholic")]
        public bool IsAlcoholic { get; set; }
        public string StrGlass { get; set; }
        [JsonProperty("strInstructions")]
        public string StrInstructions { get; set; }
        [JsonProperty("strDrinkThumb")]
        public string PictureLink { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Measurements { get; set; }
        public string DateModified { get; set; }
    }
}
