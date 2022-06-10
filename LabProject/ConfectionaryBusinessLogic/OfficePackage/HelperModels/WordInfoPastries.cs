using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoPastries : WordInfoAbstract
    {
        public List<PastryViewModel> Pastries { get; set; }
    }
}
