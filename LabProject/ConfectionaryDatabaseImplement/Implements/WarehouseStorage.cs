using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfectionaryDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using var context = new ConfectionaryDatabase();
            return context.Warehouses.Include(rec => rec.WarehouseComponents).ThenInclude(rec => rec.Component)
                .ToList().Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectionaryDatabase();
            return context.Warehouses.Include(rec => rec.WarehouseComponents).ThenInclude(rec => rec.Component)
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList().Select(CreateModel).ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectionaryDatabase();
            var warehouse = context.Warehouses.Include(rec => rec.WarehouseComponents).ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Warehouse warehouse = new Warehouse();
                CreateModel(model, warehouse);
                context.Warehouses.Add(warehouse);
                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WarehouseBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                MakeDependences(model, element, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Warehouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public bool IsEnough(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach(var comp in context.PastryComponents.Where(rec => rec.PastryId == model.PastryId))
                {
                    int required = model.Count * comp.Count;
                    foreach (var ware in context.WarehouseComponents.Where(rec => rec.ComponentId == comp.ComponentId))
                    {
                        if (required > ware.Count)
                        {
                            required -= ware.Count;
                            ware.Count = 0;
                        }
                        else
                        {
                            ware.Count -= required;
                            required = 0;
                            break;
                        }
                    }
                    if (required > 0) throw new Exception("Недостаточно компонентов");
                }
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
            return true;
        }

        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.Name = model.Name;
            warehouse.Manager = model.Manager;
            warehouse.DateCreate = model.DateCreate;
            return warehouse;
        }

        private static void MakeDependences(WarehouseBindingModel model, Warehouse warehouse, ConfectionaryDatabase context)
        {
            if (model.Id.HasValue)
            {
                var warehouseComponents = context.WarehouseComponents.
                    Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                context.WarehouseComponents.RemoveRange(warehouseComponents.
                    Where(rec => !model.StoredComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach (var updateComponent in warehouseComponents)
                {
                    updateComponent.Count = model.StoredComponents[updateComponent.ComponentId].Item2;
                    model.StoredComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.StoredComponents)
            {
                context.WarehouseComponents.Add(new WarehouseComponent
                {
                    WarehouseId = warehouse.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
        }

        private static WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Manager = warehouse.Manager,
                DateCreate = warehouse.DateCreate,
                StoredComponents = warehouse.WarehouseComponents.ToDictionary(recWC => recWC.ComponentId,
                    recWC => (recWC.Component?.ComponentName, recWC.Count))
            };
        }
    }
}
