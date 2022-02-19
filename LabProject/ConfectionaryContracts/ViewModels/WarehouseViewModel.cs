using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ConfectionaryContracts.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Заведующий")]
        public string Manager { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public Dictionary<int, (string, int)> StoredComponents { get; set; }
    }
}
