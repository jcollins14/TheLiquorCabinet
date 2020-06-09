using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailDbTools.Models
{
    public class DrinkResponse
    {
        public DrinkDb ResponseDrink { get; set; }
        public DrinkResponse(string ApiText)
        {
            JObject parse = JObject.Parse(ApiText);
            foreach (var item in parse["drinks"])
            {
                this.ResponseDrink = (new DrinkDb
                {
                    idDrink = (string)item["idDrink"],
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
}
