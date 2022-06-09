using System;
using System.Collections.Generic;
using System.Text;

namespace ConfectionaryContracts.Enums
{
    public enum OrderStatus
    {
        Принят = 0,
        Выполняется = 1,
        Требуются_материалы = 2,
        Готов = 3,
        Выдан = 4
    }
}
