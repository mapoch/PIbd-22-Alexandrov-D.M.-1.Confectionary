using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoDates : PdfInfoAbstract
    {
        public List<ReportDatesViewModel> Dates { get; set; }
    }
}
