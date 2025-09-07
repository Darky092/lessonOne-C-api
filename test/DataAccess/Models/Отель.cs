using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Отель
{
    public int КодОтеля { get; set; }

    public string НазваниеОтеля { get; set; } = null!;

    public string Улица { get; set; } = null!;

    public int НомерДома { get; set; }

    public string Город { get; set; } = null!;

    public string? Доступность { get; set; }

    public virtual ICollection<Номер> Номерs { get; set; } = new List<Номер>();
}
