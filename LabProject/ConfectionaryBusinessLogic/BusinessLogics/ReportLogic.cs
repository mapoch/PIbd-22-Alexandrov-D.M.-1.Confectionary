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
        private readonly IPastryStorage pastryStorage;
        private readonly IOrderStorage orderStorage;
        private readonly IWarehouseStorage warehouseStorage;
        private readonly AbstractSaveToExcel saveToExcel;
        private readonly AbstractSaveToWord saveToWord;
        private readonly AbstractSaveToPdf saveToPdf;
        public ReportLogic(IPastryStorage _pastryStorage, IComponentStorage _componentStorage, 
            IOrderStorage _orderStorage, IWarehouseStorage _warehouseStorage, 
            AbstractSaveToExcel _saveToExcel, 
            AbstractSaveToWord _saveToWord, AbstractSaveToPdf _saveToPdf)
        {
            pastryStorage = _pastryStorage;
            componentStorage = _componentStorage;
            orderStorage = _orderStorage;
            warehouseStorage = _warehouseStorage;
            saveToExcel = _saveToExcel;
            saveToWord = _saveToWord;
            saveToPdf = _saveToPdf;
        }

        public List<ReportPastryComponentViewModel> GetPastryComponent()
        {
            var pastries = pastryStorage.GetFullList();
            var list = new List<ReportPastryComponentViewModel>();
            foreach (var pastry in pastries)
            {
                var record = new ReportPastryComponentViewModel
                {
                    PastryName = pastry.PastryName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in pastry.PastryComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1,
                        component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public List<ReportComponentPastryViewModel> GetComponentPastry()
        {
            var pastries = pastryStorage.GetFullList();
            var list = new List<ReportComponentPastryViewModel>();
            foreach (var pastry in pastries)
            {
                foreach (var component in pastry.PastryComponents)
                {
                    ReportComponentPastryViewModel record = list.Find(rec => rec.ComponentName == component.Value.Item1);
                    if (record != null)
                    {
                        Tuple<string, int> storedP = record.Pastries.Find(rec => rec.Item1 == pastry.PastryName);
                        if (storedP != null)
                        {
                            record.Pastries.Remove(storedP);
                            record.Pastries.Add(new Tuple<string, int>(storedP.Item1, storedP.Item2 + component.Value.Item2));
                            record.TotalCount += component.Value.Item2;
                        }
                        else
                        {
                            record.Pastries.Add(new Tuple<string, int>(pastry.PastryName, component.Value.Item2));
                            record.TotalCount += component.Value.Item2;
                        }
                    }
                    else
                    {
                        List<Tuple<string, int>> pastriesList = new List<Tuple<string, int>>();
                        pastriesList.Add(new Tuple<string, int>(pastry.PastryName, component.Value.Item2));
                        list.Add(new ReportComponentPastryViewModel
                        {
                            ComponentName = component.Value.Item1,
                            TotalCount = component.Value.Item2,
                            Pastries = pastriesList
                        });
                    }
                }
            }
            return list;
        }

        public List<ReportWarehouseComponentViewModel> GetWarehouseComponent()
        {
            var warehouses = warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseComponentViewModel>();
            foreach (var warehouse in warehouses)
            {
                var record = new ReportWarehouseComponentViewModel
                {
                    WarehouseName = warehouse.Name,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in warehouse.StoredComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1,
                        component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
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

        public List<ReportDatesViewModel> GetDates(ReportBindingModel model)
        {
            var list = orderStorage.GetFullList().GroupBy(rec => rec.DateCreate.Date).
                Select(rec => new ReportDatesViewModel { Date = rec.Key.Date.Date, Count = rec.Count(), Sum = rec.Sum(order => order.Sum) }).ToList();
            return list;
        }

        public void SavePastriesToWordFile(ReportBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfoPastries
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Pastries = pastryStorage.GetFullList()
            });
        }

        public void SaveWarehousesToWordFile(ReportBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfoWarehouses
            {
                FileName = model.FileName,
                Title = "Список складов",
                Warehouses = warehouseStorage.GetFullList()
            });
        }

        public void SavePastryComponentToExcelFile(ReportBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfoPastryComponent
            {
                FileName = model.FileName,
                Title = "Список изделий",
                PastryComponents = GetPastryComponent()
            });
        }

        public void SaveWarehouseComponentToExcelFile(ReportBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfoWarehouseComponent
            {
                FileName = model.FileName,
                Title = "Заполненность складов",
                WarehouseComponents = GetWarehouseComponent()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            saveToPdf.CreateDoc(new PdfInfoOrders
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public void SaveDatesToPdfFile(ReportBindingModel model)
        {
            saveToPdf.CreateDoc(new PdfInfoDates
            {
                FileName = model.FileName,
                Title = "Список дат",
                Dates = GetDates(model)
            });
        }
    }
}
