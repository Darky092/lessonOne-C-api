using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Услуга
{
    public int КодУслуги { get; set; }

    public decimal ЦенаУслуги { get; set; }

    public string НазваниеУслуги { get; set; } = null!;

    public virtual ICollection<Тариф> КодТарифаs { get; set; } = new List<Тариф>();
}
