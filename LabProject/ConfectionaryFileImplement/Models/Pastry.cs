using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryFileImplement.Models
{
    public class Pastry
    {
        public int Id { get; set; }
        public string PastryName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> PastryComponents { get; set; }
    }
}
