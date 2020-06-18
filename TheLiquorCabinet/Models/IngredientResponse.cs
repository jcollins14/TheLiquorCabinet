using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class IngredientResponse
    {
        public IngredDb ResponseIngred { get; set; }
        public IngredientResponse(string ApiText)
        {
            JObject parse = JObject.Parse(ApiText);
            foreach (var item in parse["ingredients"])
            {
                this.ResponseIngred = (new IngredDb
                {
                    Id = (int)item["idIngredient"],
                    Name = (string)item["strIngredient"],
                    Type = (string)item["strType"],
                    Alcohol = (string)item["strAlcohol"],
                    ABV = (string)item["strABV"],
                });
            }
        }
    }
}
