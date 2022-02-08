using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.StoragesContracts
{
    public interface IPastryStorage
    {
        List<PastryViewModel> GetFullList();
        List<PastryViewModel> GetFilteredList(PastryBindingModel model);
        PastryViewModel GetElement(PastryBindingModel model);
        void Insert(PastryBindingModel model);
        void Update(PastryBindingModel model);
        void Delete(PastryBindingModel model);
    }
}
