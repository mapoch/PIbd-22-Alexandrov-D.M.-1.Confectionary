using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryListImplement.Models;

namespace ConfectionaryListImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pastry> Pastries { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Pastries = new List<Pastry>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null) instance = new DataListSingleton();

            return instance;
        }
    }
}
