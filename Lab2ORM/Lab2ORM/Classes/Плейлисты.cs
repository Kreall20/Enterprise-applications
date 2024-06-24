using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Плейлисты
{
    public int КодПлейлиста { get; set; }

    public int UserId { get; set; }

    public string Плейлист { get; set; } = null!;

    public virtual Пользователи User { get; set; } = null!;

    public virtual ICollection<ПесниПлейлистов> ПесниПлейлистовs { get; } = new List<ПесниПлейлистов>();
}
