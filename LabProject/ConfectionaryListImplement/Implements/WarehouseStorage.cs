using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryListImplement.Models;

namespace ConfectionaryListImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;

        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            var result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                result.Add(CreateModel(warehouse));
            }
            return result;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null) return null;

            var result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(warehouse));
                }
            }
            return result;
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null) return null;

            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            var tempWarehouse = new Warehouse { Id = 1, StoredComponents = new Dictionary<int, int>() };
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id >= tempWarehouse.Id) tempWarehouse.Id = warehouse.Id + 1;
            }
            source.Warehouses.Add(CreateModel(model, tempWarehouse));
        }

        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = null;
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id) tempWarehouse = warehouse;
            }

            if (tempWarehouse == null) throw new Exception("Элемент не найден");

            CreateModel(model, tempWarehouse);
        }

        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public bool IsEnough(OrderBindingModel order)
        {
            return false;
        }

        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.Name = model.Name;
            warehouse.Manager = model.Manager;
            warehouse.DateCreate = model.DateCreate;
            
            foreach (var key in warehouse.StoredComponents.Keys)
            {
                if (!model.StoredComponents.ContainsKey(key))
                {
                    warehouse.StoredComponents.Remove(key);
                }
            }
            if (model.StoredComponents != null) foreach (var component in model.StoredComponents)
            {
                if (warehouse.StoredComponents.ContainsKey(component.Key))
                {
                    warehouse.StoredComponents[component.Key] = model.StoredComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.StoredComponents.Add(component.Key, model.StoredComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            var warehouseComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in warehouse.StoredComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                warehouseComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Manager = warehouse.Manager,
                DateCreate = warehouse.DateCreate,
                StoredComponents = warehouseComponents
            };
        }
    }
}
