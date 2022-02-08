using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryListImplement.Models
{
    class Pastry
    {
        public int Id { get; set; }
        public string PastryName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> PastryComponents { get; set; }
    }
}
