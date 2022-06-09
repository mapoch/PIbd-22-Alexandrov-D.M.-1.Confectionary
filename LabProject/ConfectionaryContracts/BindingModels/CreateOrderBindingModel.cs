using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryContracts.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int PastryId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public int ClientId { get; set; }
    }
}
