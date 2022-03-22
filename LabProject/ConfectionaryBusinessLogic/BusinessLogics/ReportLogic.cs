using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryBusinessLogic.OfficePackage;
using ConfectionaryBusinessLogic.OfficePackage.HelperModels;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage componentStorage;
        private readonly IPastryStorage productStorage;
        private readonly IOrderStorage orderStorage;
        private readonly AbstractSaveToExcel saveToExcel;
        private readonly AbstractSaveToWord saveToWord;
        private readonly AbstractSaveToPdf saveToPdf;
        public ReportLogic(IPastryStorage _productStorage, IComponentStorage _componentStorage, 
            IOrderStorage _orderStorage, AbstractSaveToExcel _saveToExcel, 
            AbstractSaveToWord _saveToWord, AbstractSaveToPdf _saveToPdf)
        {
            productStorage = _productStorage;
            componentStorage = _componentStorage;
            orderStorage = _orderStorage;
            saveToExcel = _saveToExcel;
            saveToWord = _saveToWord;
            saveToPdf = _saveToPdf;
        }

        public List<ReportPastryComponentViewModel> GetPastryComponent()
        {
            var components = componentStorage.GetFullList();
            var products = productStorage.GetFullList();
            var list = new List<ReportPastryComponentViewModel>();
            foreach (var component in components)
            {
                var record = new ReportPastryComponentViewModel
                {
                    ComponentName = component.ComponentName,
                    Pastries = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var product in products)
                {
                    if (product.PastryComponents.ContainsKey(component.Id))
                    {
                        record.Pastries.Add(new Tuple<string, int>(product.PastryName, 
                            product.PastryComponents[component.Id].Item2));
                        record.TotalCount += product.PastryComponents[component.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderStorage.GetFilteredList(new OrderBindingModel 
            { DateFrom = model.DateFrom, DateTo = model.DateTo }).Select(x => new ReportOrdersViewModel
            { DateCreate = x.DateCreate, PastryName = x.PastryName, Count = x.Count, Sum = x.Sum, Status = x.Status })
           .ToList();
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Components = componentStorage.GetFullList()
            });
        }

        public void SavePastryComponentToExcelFile(ReportBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                PastryComponents = GetPastryComponent()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
