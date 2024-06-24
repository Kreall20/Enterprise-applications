using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Исполнители
{
    public int КодИсполнителя { get; set; }

    public string? Исполнитель { get; set; }

    public virtual ICollection<Песни> Песниs { get; } = new List<Песни>();
}
