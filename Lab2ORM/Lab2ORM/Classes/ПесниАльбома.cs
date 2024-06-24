using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class ПесниАльбома
{
    public int КодАльбома { get; set; }

    public int КодПесни { get; set; }

    public int Id { get; set; }

    public virtual Альбомы КодАльбомаNavigation { get; set; } = null!;

    public virtual Песни КодПесниNavigation { get; set; } = null!;
}
