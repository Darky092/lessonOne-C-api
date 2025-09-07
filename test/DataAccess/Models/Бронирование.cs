using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Бронирование
{
    public int КодБронирования { get; set; }

    public bool ФактОплаты { get; set; }

    public DateOnly ДатаВыезда { get; set; }

    public DateOnly ДатаВъезда { get; set; }

    public virtual ICollection<Клиенты> КодКлиентаs { get; set; } = new List<Клиенты>();

    public virtual ICollection<ТарифыНомеров> ТарифыНомеровs { get; set; } = new List<ТарифыНомеров>();
}
