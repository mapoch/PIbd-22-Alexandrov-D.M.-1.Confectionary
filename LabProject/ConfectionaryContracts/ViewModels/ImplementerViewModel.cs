using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.Attributes;

namespace ConfectionaryContracts.ViewModels
{
    public class ImplementerViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Имя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FIO { get; set; }

        [Column(title: "Время работы", width: 50)]
        public int WorkingTime { get; set; }

        [Column(title: "Время отдыха", width: 50)]
        public int PauseTime { get; set; }
    }
}
