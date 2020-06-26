using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TheLiquorCabinet.Models
{
    public class Drink
    {
        [JsonProperty("idDrink")]
        public int ID { get; set; }
        [JsonProperty("strDrink")]
        public string Name { get; set; }
        public string Category { get; set; }
        public string IBA { get; set; }
        [JsonProperty("strAlcoholic")]
        public bool IsAlcoholic { get; set; }
        public string Glass { get; set; }
        [JsonProperty("strInstructions")]
        public string Instructions { get; set; }
        [JsonProperty("strDrinkThumb")]
        public string PictureLink { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Measurements { get; set; }
        [NotMapped]
        public List<bool> IngredAvail { get; set; }
        public string DateModified { get; set; }

        public Drink(string APItext)
        {
            this.Ingredients = new List<string>();
            this.Measurements = new List<string>();
            JObject parse = JObject.Parse(APItext);
            this.ID = (int)parse["drinks"][0]["idDrink"];
            this.Name = (string)parse["drinks"][0]["strDrink"];
            this.Category = (string)parse["drinks"][0]["strCategory"];
            this.IBA = (string)parse["drinks"][0]["strIBA"];
            if ((string)parse["drinks"][0]["strAlcoholic"] == "Alcoholic")
            {
                this.IsAlcoholic = true;
            }
            else
            {
                this.IsAlcoholic = false;
            }
            this.Glass = (string)parse["drinks"][0]["strGlass"];
            this.Instructions = (string)parse["drinks"][0]["strInstructions"];
            this.PictureLink = (string)parse["drinks"][0]["strDrinkThumb"];
            for (int i = 0; i < 15; i++)
            {
                string ingredient = (string)parse["drinks"][0]["strIngredient" + i];
                if (ingredient != null)
                {
                    this.Ingredients.Add(ingredient);
                }
            }
            for (int i = 0; i < this.Ingredients.Count; i++)
            {
                string measure = (string)parse["drinks"][0]["strMeasure" + i];
                if (measure != null)
                {
                    this.Measurements.Add(measure);
                }
            }
            this.DateModified = (string)parse["drinks"][0]["dateModified"];
            this.IngredAvail = new List<bool>();
        }
    }
}
