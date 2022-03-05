using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.StoragesContracts
{
    public interface IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList();
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model);
        public WarehouseViewModel GetElement(WarehouseBindingModel model);
        public void Insert(WarehouseBindingModel model);
        public void Update(WarehouseBindingModel model);
        public void Delete(WarehouseBindingModel model);
    }
}
