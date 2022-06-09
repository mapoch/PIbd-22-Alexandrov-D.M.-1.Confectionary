using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly IImplementerStorage implementerStorage;

        public ImplementerLogic(IImplementerStorage _implementerStorage)
        {
            implementerStorage = _implementerStorage;
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return implementerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ImplementerViewModel> { implementerStorage.GetElement(model) };
            }
            return implementerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            var element = implementerStorage.GetElement(new ImplementerBindingModel { FIO = model.FIO });

            if (element != null && element.Id != model.Id) throw new Exception("Уже есть клиент с таким логином");

            if (model.Id.HasValue)
            {
                implementerStorage.Update(model);
            }
            else
            {
                implementerStorage.Insert(model);
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            var element = implementerStorage.GetElement(new ImplementerBindingModel { Id = model.Id });

            if (element == null) throw new Exception("Элемент не найден");

            implementerStorage.Delete(model);
        }
    }
}
