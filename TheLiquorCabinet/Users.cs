using System;
using System.Collections.Generic;

namespace TheLiquorCabinet
{
    public partial class Users
    {
        public Users()
        {
            Cabinet = new HashSet<Cabinet>();
            Favorites = new HashSet<Favorites>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Cabinet> Cabinet { get; set; }
        public virtual ICollection<Favorites> Favorites { get; set; }
    }
}
