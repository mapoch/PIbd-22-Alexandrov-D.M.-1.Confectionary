using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryContracts.BindingModels
{
    public class ReplenishBindingModel
    {
        public int WarehouseId { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int Count { get; set; }
    }
}
