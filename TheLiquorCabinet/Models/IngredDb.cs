using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheLiquorCabinet.Models
{
    public class IngredDb
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Alcohol { get; set; }
        public string ABV { get; set; }
    }
}
