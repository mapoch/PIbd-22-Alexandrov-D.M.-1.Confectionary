using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage warehouseStorage;

        public WarehouseLogic(IWarehouseStorage _warehouseStorage)
        {
            warehouseStorage = _warehouseStorage;
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { warehouseStorage.GetElement(model) };
            }
            return warehouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = warehouseStorage.GetElement(new WarehouseBindingModel { Name = model.Name });

            if (model.Id.HasValue)
            {
                warehouseStorage.Update(model);
            }
            else
            {
                model.DateCreate = DateTime.Now;
                warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            var element = warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });

            if (element == null) throw new Exception("Элемент не найден");

            warehouseStorage.Delete(model);
        }

        public void AddComponents(ReplenishBindingModel model)
        {
            var element = warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.WarehouseId });

            if (element == null) throw new Exception("Элемент не найден");

            (string, int) components;

            element.StoredComponents.TryGetValue(model.ComponentId, out components);

            if (components.Item1 != null)
            {
                components.Item2 = model.Count + components.Item2;
                element.StoredComponents.Remove(model.ComponentId);
                element.StoredComponents.Add(model.ComponentId, components);
            }
            else element.StoredComponents.Add(model.ComponentId, (model.ComponentName, model.Count));

            warehouseStorage.Update(new WarehouseBindingModel { 
                Id = model.WarehouseId, Name = element.Name, Manager = element.Manager, DateCreate = element.DateCreate,
                StoredComponents = element.StoredComponents});
        }
    }
}
