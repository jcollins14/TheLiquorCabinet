using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
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
                    IdDrink = (int)item["idDrink"],
                    StrDrink = (string)item["strDrink"],
                    StrIngredient1 = (string)item["strIngredient1"],
                    StrIngredient2 = (string)item["strIngredient2"],
                    StrIngredient3 = (string)item["strIngredient3"],
                    StrIngredient4 = (string)item["strIngredient4"],
                    StrIngredient5 = (string)item["strIngredient5"],
                    StrIngredient6 = (string)item["strIngredient6"],
                    StrIngredient7 = (string)item["strIngredient7"],
                    StrIngredient8 = (string)item["strIngredient8"],
                    StrIngredient9 = (string)item["strIngredient9"],
                    StrIngredient10 = (string)item["strIngredient10"],
                    StrIngredient11 = (string)item["strIngredient11"],
                    StrIngredient12 = (string)item["strIngredient12"],
                    StrIngredient13 = (string)item["strIngredient13"],
                    StrIngredient14 = (string)item["strIngredient14"],
                    StrIngredient15 = (string)item["strIngredient15"]
                });
            }
        }
    }
}
