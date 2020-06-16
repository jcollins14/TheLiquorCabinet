using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class IngredOnHand
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int IngredID { get; set; }
        public User User { get; set; }

        public IngredOnHand()
        {

        }

        public IngredOnHand(int id, int userID)
        {
            this.IngredID = id;
            this.UserID = userID;
        }

        public IngredOnHand(string APIText, int userID)
        {
            JObject parse = JObject.Parse(APIText);
            this.IngredID = (int)parse["ingredients"][0]["idIngredient"];
            this.UserID = userID;
        }
    }
}
