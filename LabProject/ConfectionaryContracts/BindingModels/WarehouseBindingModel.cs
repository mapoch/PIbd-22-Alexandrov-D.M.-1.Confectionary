using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryContracts.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, (string, int)> StoredComponents { get; set; }
    }
}
