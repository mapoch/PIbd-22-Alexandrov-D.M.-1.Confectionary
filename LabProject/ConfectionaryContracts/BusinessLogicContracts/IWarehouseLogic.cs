using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.BusinessLogicContracts
{
    public interface IWarehouseLogic
    {
        public List<WarehouseViewModel> Read(WarehouseBindingModel model);

        public void CreateOrUpdate(WarehouseBindingModel model);

        public void Delete(WarehouseBindingModel model);

        public void AddComponents(ReplenishBindingModel model);
    }
}
