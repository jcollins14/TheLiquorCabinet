
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheLiquorCabinet.Models
{
    public class Ingredient
    {
        [JsonProperty("idIngredient")]
        public int ID { get; set; }
        [JsonProperty("strIngredient")]
        public string Name { get; set; }
        [JsonProperty("strDescription")]
        public string Description { get; set; }
        [JsonProperty("strType")]
        public string Type { get; set; }
        public bool IsAlcoholic { get; set; }
        [JsonProperty("strABV")]
        public int ABV { get; set; }

        public Ingredient(string APItext)
        {
            JObject parse = JObject.Parse(APItext);
            this.ID = parse.Property("idIngredient").ToObject<int>();
            this.Name = parse.Property("strIngredient").ToObject<string>();
            this.Description = parse.Property("strDescription").ToObject<string>();
            this.Type = parse.Property("strType").ToObject<string>();
            if (parse.Property("strAlcohol").ToObject<string>() == "Yes")
            {
                this.IsAlcoholic = true;
            }
            else
            {
                this.IsAlcoholic = false;
            }
            this.ABV = parse.Property("strABV").ToObject<int>();
        }
    }
}
