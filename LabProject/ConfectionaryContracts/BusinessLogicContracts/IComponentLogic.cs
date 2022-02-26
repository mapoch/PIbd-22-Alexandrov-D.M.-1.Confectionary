using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.BusinessLogicContracts
{
    public interface IComponentLogic
    {
        List<ComponentViewModel> Read(ComponentBindingModel model);

        void CreateOrUpdate(ComponentBindingModel model);

        void Delete(ComponentBindingModel model);
    }
}
