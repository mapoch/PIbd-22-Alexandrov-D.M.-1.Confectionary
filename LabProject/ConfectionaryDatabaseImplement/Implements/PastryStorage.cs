﻿using System;
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
    public class PastryStorage : IPastryStorage
    {
        public List<PastryViewModel> GetFullList()
        {
            using var context = new ConfectionaryDatabase();
            return context.Pastries.Include(rec => rec.PastryComponents).ThenInclude(rec => rec.Component)
                .ToList().Select(CreateModel).ToList();
        }

        public List<PastryViewModel> GetFilteredList(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectionaryDatabase();
            return context.Pastries.Include(rec => rec.PastryComponents).ThenInclude(rec => rec.Component)
                .Where(rec => rec.PastryName.Contains(model.PastryName))
                .ToList().Select(CreateModel).ToList();
        }

        public PastryViewModel GetElement(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new ConfectionaryDatabase();
            var pastry = context.Pastries.Include(rec => rec.PastryComponents).ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.PastryName == model.PastryName || rec.Id == model.Id);
            return pastry != null ? CreateModel(pastry) : null;
        }

        public void Insert(PastryBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Pastries.Add(CreateModel(model, new Pastry(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(PastryBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Pastries.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(PastryBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            Pastry element = context.Pastries.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Pastries.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Pastry CreateModel(PastryBindingModel model, Pastry pastry, ConfectionaryDatabase context) 
        {
            pastry.PastryName = model.PastryName;
            pastry.Price = model.Price;

            if (model.Id.HasValue)
            {
                var pastryComponents = context.PastryComponents.
                    Where(rec => rec.PastryId == model.Id.Value).ToList();
                context.PastryComponents.RemoveRange(pastryComponents.
                    Where(rec => !model.PastryComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                foreach(var updateComponent in pastryComponents)
                {
                    updateComponent.Count = model.PastryComponents[updateComponent.ComponentId].Item2;
                    model.PastryComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            foreach(var pc in model.PastryComponents)
            {
                context.PastryComponents.Add(new PastryComponent
                {
                    PastryId = pastry.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return pastry;
        }

        private static PastryViewModel CreateModel(Pastry pastry)
        {
            return new PastryViewModel
            {
                Id = pastry.Id,
                PastryName = pastry.PastryName,
                Price = pastry.Price,
                PastryComponents = pastry.PastryComponents.ToDictionary(recPC => recPC.ComponentId, 
                    recPC => (recPC.Component?.ComponentName, recPC.Count))
            };
        }
    }
}
