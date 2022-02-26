using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class PastryLogic : IPastryLogic
    {
        private readonly IPastryStorage pastryStorage;

        public PastryLogic(IPastryStorage _pastryStorage)
        {
            pastryStorage = _pastryStorage;
        }

        public List<PastryViewModel> Read(PastryBindingModel model)
        {
            if (model == null)
            {
                return pastryStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PastryViewModel> { pastryStorage.GetElement(model) };
            }
            return pastryStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PastryBindingModel model)
        {
            var element = pastryStorage.GetElement(new PastryBindingModel { PastryName = model.PastryName });

            if (element != null && element.Id != model.Id) throw new Exception("Уже есть компонент с таким названием");

            if (model.Id.HasValue)
            {
                pastryStorage.Update(model);
            }
            else
            {
                pastryStorage.Insert(model);
            }
        }

        public void Delete(PastryBindingModel model)
        {
            var element = pastryStorage.GetElement(new PastryBindingModel { Id = model.Id });

            if (element == null) throw new Exception("Элемент не найден");

            pastryStorage.Delete(model);
        }
    }
}
