using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConfectionaryContracts.ViewModels
{
    public class ClientViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Имя")]
        public string FIO { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
