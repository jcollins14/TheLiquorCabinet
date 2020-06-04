using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public List<int> Favorites { get; set; }
        public List<Ingredient> Cabinet { get; set; }
        public User(string username)
        {
            this.Username = username;
            this.Favorites = new List<int>();
            this.Cabinet = new List<Ingredient>();
        }
    }
}
