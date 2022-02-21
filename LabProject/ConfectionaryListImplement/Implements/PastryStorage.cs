using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryListImplement.Models;

namespace ConfectionaryListImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly DataListSingleton source;

        public PastryStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<PastryViewModel> GetFullList()
        {
            var result = new List<PastryViewModel>();
            foreach(var pastry in source.Pastries)
            {
                result.Add(CreateModel(pastry));
            }
            return result;
        }

        public List<PastryViewModel> GetFilteredList(PastryBindingModel model)
        {
            if (model == null) return null;

            var result = new List<PastryViewModel>();
            foreach(var pastry in source.Pastries)
            {
                if (pastry.PastryName.Contains(model.PastryName))
                {
                    result.Add(CreateModel(pastry));
                }
            }
            return result;
        }

        public PastryViewModel GetElement(PastryBindingModel model)
        {
            if (model == null) return null;

            foreach(var pastry in source.Pastries)
            {
                if (pastry.Id == model.Id || pastry.PastryName == model.PastryName)
                {
                    return CreateModel(pastry);
                }
            }
            return null;
        }

        public void Insert(PastryBindingModel model)
        {
            var tempPastry = new Pastry { Id = 1, PastryComponents = new Dictionary<int, int>() };
            foreach(var pastry in source.Pastries)
            {
                if (pastry.Id >= tempPastry.Id) tempPastry.Id = pastry.Id + 1;
            }
            source.Pastries.Add(CreateModel(model, tempPastry));
        }

        public void Update(PastryBindingModel model)
        {
            Pastry tempPastry = null;
            foreach(var pastry in source.Pastries)
            {
                if (pastry.Id == model.Id) tempPastry = pastry;
            }

            if (tempPastry == null) throw new Exception("Элемент не найден");

            CreateModel(model, tempPastry);
        }

        public void Delete(PastryBindingModel model)
        {
            for (int i = 0; i < source.Pastries.Count; ++i)
            {
                if (source.Pastries[i].Id == model.Id)
                {
                    source.Pastries.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Pastry CreateModel(PastryBindingModel model, Pastry pastry)
        {
            pastry.PastryName = model.PastryName;
            pastry.Price = model.Price;

            foreach(var key in pastry.PastryComponents.Keys)
            {
                if (!model.PastryComponents.ContainsKey(key))
                {
                    pastry.PastryComponents.Remove(key);
                }
            }
            foreach(var component in model.PastryComponents)
            {
                if (pastry.PastryComponents.ContainsKey(component.Key))
                {
                    pastry.PastryComponents[component.Key] = model.PastryComponents[component.Key].Item2;
                }
                else
                {
                    pastry.PastryComponents.Add(component.Key, model.PastryComponents[component.Key].Item2);
                }
            }
            return pastry;
        }

        private PastryViewModel CreateModel(Pastry product)
        {
            var pastryComponents = new Dictionary<int, (string, int)>();
            foreach(var pc in product.PastryComponents)
            {
                string componentName = string.Empty;
                foreach(var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                pastryComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new PastryViewModel { Id = product.Id, PastryName = product.PastryName, 
                Price = product.Price, PastryComponents = pastryComponents };
        }
    }
}
