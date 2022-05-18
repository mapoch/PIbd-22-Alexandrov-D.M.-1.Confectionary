using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using ConfectionaryContracts.Attributes;

namespace ConfectionaryContracts.ViewModels
{
    public class PastryViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string PastryName { get; set; }

        [Column(title: "Цена", width: 50)]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> PastryComponents { get; set; }
    }
}
