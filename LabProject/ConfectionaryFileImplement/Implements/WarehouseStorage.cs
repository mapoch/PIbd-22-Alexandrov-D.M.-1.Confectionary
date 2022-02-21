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
            source = FileDataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null) return null;
            return source.Warehouses.Where(rec => rec.Name.Contains(model.Name))
                .Select(CreateModel).ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null) return null;
            var warehouse = source.Warehouses
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var element = new Warehouse { Id = maxId + 1, StoredComponents = new Dictionary<int, int>() };
            source.Warehouses.Add(CreateModel(model, element));
        }

        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Элемент не найден");
            CreateModel(model, element);
        }

        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null) source.Warehouses.Remove(element);
            else throw new Exception("Элемент не найден");
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
