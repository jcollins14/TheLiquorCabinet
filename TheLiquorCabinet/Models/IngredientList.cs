<<<<<<< HEAD
﻿using System;
=======
﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
>>>>>>> dev
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
<<<<<<< HEAD
    public class IngredientList
    {

=======
    //This model parses the API response for a list of ingredients to a list of ingredient names.
    public class IngredientList
    {
        public List<string> IngredientNames { get; set; }
        public IngredientList(string APIText)
        {
            this.IngredientNames = new List<string>();
            JObject parse = JObject.Parse(APIText);
            foreach (var item in parse["drinks"])
            {
                this.IngredientNames.Add(item["strIngredient1"].ToString());
            }
        }

        public IngredientList()
        {
        }
>>>>>>> dev
    }
}
