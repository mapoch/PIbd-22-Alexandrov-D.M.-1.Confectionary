﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryFileImplement.Models;

namespace ConfectionaryFileImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly FileDataListSingleton source;

        public PastryStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<PastryViewModel> GetFullList()
        {
            return source.Pastries.Select(CreateModel).ToList();
        }

        public List<PastryViewModel> GetFilteredList(PastryBindingModel model)
        {
            if (model == null) return null;
            return source.Pastries.Where(rec => rec.PastryName.Contains(model.PastryName))
                .Select(CreateModel).ToList();
        }

        public PastryViewModel GetElement(PastryBindingModel model)
        {
            if (model == null) return null;
            var pastry = source.Pastries
                .FirstOrDefault(rec => rec.PastryName == model.PastryName || rec.Id == model.Id);
            return pastry != null ? CreateModel(pastry) : null;
        }

        public void Insert(PastryBindingModel model)
        {
            int maxId = source.Pastries.Count > 0 ? source.Pastries.Max(rec => rec.Id) : 0;
            var element = new Pastry { Id = maxId + 1, PastryComponents = new Dictionary<int, int>() };
            source.Pastries.Add(CreateModel(model, element));
        }

        public void Update(PastryBindingModel model)
        {
            var element = source.Pastries.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null) throw new Exception("Элемент не найден");
            CreateModel(model, element);
        }

        public void Delete(PastryBindingModel model)
        {
            Pastry element = source.Pastries.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null) source.Pastries.Remove(element);
            else throw new Exception("Элемент не найден");
        }

        private static Pastry CreateModel(PastryBindingModel model, Pastry pastry)
        {
            pastry.PastryName = model.PastryName;
            pastry.Price = model.Price;

            foreach (var key in pastry.PastryComponents.Keys.ToList())
            {
                if (!model.PastryComponents.ContainsKey(key))
                    pastry.PastryComponents.Remove(key);
            }
            foreach (var component in model.PastryComponents)
            {
                if (pastry.PastryComponents.ContainsKey(component.Key))
                    pastry.PastryComponents[component.Key] = model.PastryComponents[component.Key].Item2;
                else
                    pastry.PastryComponents.Add(component.Key, model.PastryComponents[component.Key].Item2);
            }
            return pastry;
        }

        private PastryViewModel CreateModel(Pastry pastry)
        {
            return new PastryViewModel
            {
                Id = pastry.Id,
                PastryName = pastry.PastryName,
                Price = pastry.Price,
                PastryComponents = pastry.PastryComponents
                    .ToDictionary(recPC => recPC.Key, recPC => 
                        (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
            };
        }
    }
}
