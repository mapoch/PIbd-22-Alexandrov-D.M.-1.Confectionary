using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectionaryContracts.ViewModels
{
    public class ImplementerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Имя")]
        public string FIO { get; set; }

        [DisplayName("Время работы")]
        public int WorkingTime { get; set; }

        [DisplayName("Время простоя")]
        public int PauseTime { get; set; }
    }
}
