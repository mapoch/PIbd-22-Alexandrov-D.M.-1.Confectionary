using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectionaryBusinessLogic.OfficePackage.HelperModels
{
    public class WordTable
    {
        public WordTextProperties ColumnsProps { get; set; }
        public List<string> Columns { get; set; }
        public List<string[]> Texts { get; set; }
    }
}
