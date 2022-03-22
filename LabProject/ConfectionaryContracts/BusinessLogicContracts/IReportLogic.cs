using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryContracts.BusinessLogicContracts
{
    public interface IReportLogic
    {
        List<ReportPastryComponentViewModel> GetPastryComponent();
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        void SaveComponentsToWordFile(ReportBindingModel model);
        void SavePastryComponentToExcelFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
    }
}
