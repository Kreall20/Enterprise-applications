using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Альбомы
{
    public int КодАльбома { get; set; }

    public string НазваниеАльбома { get; set; } = null!;

    public int Автор { get; set; }

    public virtual ICollection<ПесниАльбома> ПесниАльбомаs { get; } = new List<ПесниАльбома>();
}
