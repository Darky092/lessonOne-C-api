using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Тариф
{
    public decimal ЦенаТарифа { get; set; }

    public int КодТарифа { get; set; }

    public string НазваниеТарифа { get; set; } = null!;

    public virtual ICollection<ТарифыНомеров> ТарифыНомеровs { get; set; } = new List<ТарифыНомеров>();

    public virtual ICollection<Услуга> КодУслугиs { get; set; } = new List<Услуга>();
}
