using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Номер
{
    public int КодНомера { get; set; }

    public int КодОтеля { get; set; }

    public int КоличествоВанныхКомнат { get; set; }

    public int КоличествоСпальныхМест { get; set; }

    public virtual Отель КодОтеляNavigation { get; set; } = null!;

    public virtual ICollection<ТарифыНомеров> ТарифыНомеровs { get; set; } = new List<ТарифыНомеров>();

    public virtual ICollection<Комуникации> КодКоммуникацииs { get; set; } = new List<Комуникации>();
}
