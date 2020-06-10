using System;
using System.Collections.Generic;

namespace TheLiquorCabinet
{
    public partial class Favorites
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DrinkId { get; set; }

        public virtual Users User { get; set; }
    }
}
