using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class ПесниПлейлистов
{
    public int Id { get; set; }

    public int КодПлейлиста { get; set; }

    public int КодПесни { get; set; }

    public virtual Песни КодПесниNavigation { get; set; } = null!;

    public virtual Плейлисты КодПлейлистаNavigation { get; set; } = null!;
}
