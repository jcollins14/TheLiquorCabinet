using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailDbTools.Models
{
    public class DrinkResponse
    {
        public List<DrinkDb> Drinks { get; set; }
        public DrinkResponse(string ApiText)
        {
            JObject parse = JObject.Parse(ApiText);
            foreach (var item in parse["drinks"])
            {
                this.Drinks.Add(new DrinkDb { idDrink = (string)item["idDrink"], 
                    strDrink = (string)item["strDrink"],
                    strIngredient1 = (string)item["strIngredient1"], 
                    strIngredient2 = (string)item["strIngredient2"],
                    strIngredient3 = (string)item["strIngredient3"], 
                    strIngredient4 = (string)item["strIngredient4"], 
                    strIngredient5 = (string)item["strIngredient5"],
                    strIngredient6 = (string)item["strIngredient6"],
                    strIngredient7 = (string)item["strIngredient7"],
                    strIngredient8 = (string)item["strIngredient8"],
                    strIngredient9 = (string)item["strIngredient9"],
                    strIngredient10 = (string)item["strIngredient10"],
                    strIngredient11 = (string)item["strIngredient11"],
                    strIngredient12 = (string)item["strIngredient12"],
                    strIngredient13 = (string)item["strIngredient13"],
                    strIngredient14 = (string)item["strIngredient14"],
                    strIngredient15 = (string)item["strIngredient15"]
                });
            }
        }
    }
    public class DrinkDb
    {
        [Key]
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }

    }

}
