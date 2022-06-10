using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoWarehouseComponent : ExcelInfoAbstract
    {
        public List<ReportWarehouseComponentViewModel> WarehouseComponents { get; set; }
    }
}
