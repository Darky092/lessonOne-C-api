using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Клиенты
{
    public int КодКлиента { get; set; }

    public string КемВыданПаспорт { get; set; } = null!;

    public int Номер { get; set; }

    public int СерияПаспорта { get; set; }

    public string? Отчество { get; set; }

    public string Фамилия { get; set; } = null!;

    public string Имя { get; set; } = null!;

    public string Пол { get; set; } = null!;

    public virtual ICollection<Бронирование> КодБронированияs { get; set; } = new List<Бронирование>();
}
