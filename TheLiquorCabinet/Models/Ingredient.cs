
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

        public Ingredient()
        {

        }

        public Ingredient(string APItext)
        {
            JObject parse = JObject.Parse(APItext);
            this.ID = (int)parse["ingredients"][0]["idIngredient"];
            this.Name = (string)parse["ingredients"][0]["strIngredient"];
            this.Description = (string)parse["ingredients"][0]["strDescription"];
            this.Type = (string)parse["ingredients"][0]["strType"];
            if ((string)parse["ingredients"][0]["strAlcohol"] == "Yes")
            {
                this.IsAlcoholic = true;
            }
            else
            {
                this.IsAlcoholic = false;
            }

            //Null check because non-alcoholic ingredients don't have an ABV

            if (!string.IsNullOrEmpty(parse["ingredients"][0]["strABV"].ToString()))
            {
                this.ABV = (int)parse["ingredients"][0]["strABV"];
            }
            
        }
    }
}
