﻿using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.Enums;

namespace ConfectionaryListImplement.Models
{
    class Order
    {
        public int Id { get; set; }
        public int PastryId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
