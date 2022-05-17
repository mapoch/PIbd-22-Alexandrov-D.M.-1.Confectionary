using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using ConfectionaryContracts.Attributes;

namespace ConfectionaryContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int? Id { get; set; }

        [Column(title: "Имя", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FIO { get; set; }

        [Column(title: "Логин", width: 150)]
        public string Login { get; set; }

        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }
    }
}
