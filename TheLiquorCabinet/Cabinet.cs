using System;
using System.Collections.Generic;

namespace TheLiquorCabinet
{
    public partial class Cabinet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IngredId { get; set; }

        public virtual Users User { get; set; }
    }
}
