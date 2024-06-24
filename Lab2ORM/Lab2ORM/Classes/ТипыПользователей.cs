using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class ТипыПользователей
{
    public int КодТипаПользователя { get; set; }

    public string ТипПользователя { get; set; } = null!;

    public virtual ICollection<Пользователи> Пользователиs { get; } = new List<Пользователи>();
}
