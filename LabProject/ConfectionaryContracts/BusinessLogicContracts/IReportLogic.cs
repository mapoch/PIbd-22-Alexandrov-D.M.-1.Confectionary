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
        List<ReportWarehouseComponentViewModel> GetWarehouseComponent();
        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        List<ReportDateViewModel> GetDates(ReportBindingModel model);
        void SavePastriesToWordFile(ReportBindingModel model);
        void SaveWarehousesToWordFile(ReportBindingModel model);
        void SavePastryComponentToExcelFile(ReportBindingModel model);
        void SaveWarehouseComponentToExcelFile(ReportBindingModel model);
        void SaveOrdersToPdfFile(ReportBindingModel model);
        void SaveDatesToPdfFile(ReportBindingModel model);
    }
}
