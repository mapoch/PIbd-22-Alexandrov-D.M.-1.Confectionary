using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.BusinessLogicContracts
{
    public interface IPastryLogic
    {
        List<PastryViewModel> Read(PastryBindingModel model);

        void CreateOrUpdate(PastryBindingModel model);

        void Delete(PastryBindingModel model);
    }
}
