using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Жанры
{
    public int КодЖанра { get; set; }

    public string Жанр { get; set; } = null!;

    public virtual ICollection<Песни> Песниs { get; } = new List<Песни>();
}
