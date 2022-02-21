using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryFileImplement.Models;

namespace ConfectionaryFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }
    }
}
