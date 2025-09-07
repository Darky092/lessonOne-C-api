using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ТарифыНомеров
{
    public int КодТарифа { get; set; }

    public int КодНомера { get; set; }

    public virtual Номер КодНомераNavigation { get; set; } = null!;

    public virtual Тариф КодТарифаNavigation { get; set; } = null!;

    public virtual ICollection<Бронирование> КодБронированияs { get; set; } = new List<Бронирование>();
}
