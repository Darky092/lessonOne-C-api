using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Комуникации
{
    public int КодКоммуникации { get; set; }

    public string НазваниеКоммуникации { get; set; } = null!;

    public virtual ICollection<Номер> КодНомераs { get; set; } = new List<Номер>();
}
