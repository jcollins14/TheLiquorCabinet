using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public DateTime Birthday { get; set; }
        [NotMapped]
        public ICollection<Favorite> Favorites { get; set; }
        [NotMapped]
        public ICollection<IngredOnHand> Cabinet { get; set; }
        public User()
        {

        }
        public User(string username)
        {
            this.Username = username;
        }

        public User(string username, int id)
        {
            this.UserID = id;
            this.Username = username;
        }
    }
}
