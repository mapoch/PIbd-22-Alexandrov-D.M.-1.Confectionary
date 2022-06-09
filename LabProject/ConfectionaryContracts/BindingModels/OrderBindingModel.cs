using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.Enums;

namespace ConfectionaryContracts.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int PastryId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ClientId { get; set; }
    }
}
