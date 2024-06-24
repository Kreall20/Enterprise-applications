using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Песни
{
    public int Автор { get; set; }

    public string НазваниеПесни { get; set; } = null!;

    public string Путь { get; set; } = null!;

    public int КодПесни { get; set; }

    public int Жанр { get; set; }

    public virtual Исполнители АвторNavigation { get; set; } = null!;

    public virtual Жанры ЖанрNavigation { get; set; } = null!;

    public virtual ICollection<ПесниАльбома> ПесниАльбомаs { get; } = new List<ПесниАльбома>();

    public virtual ICollection<ПесниПлейлистов> ПесниПлейлистовs { get; } = new List<ПесниПлейлистов>();

    public virtual ICollection<ПесниПользователя> ПесниПользователяs { get; } = new List<ПесниПользователя>();
}
