using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class Пользователи
{
    public int ТипПользователя { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int КодПользователя { get; set; }

    public virtual ICollection<ПесниПользователя> ПесниПользователяs { get; } = new List<ПесниПользователя>();

    public virtual ICollection<Плейлисты> Плейлистыs { get; } = new List<Плейлисты>();

    public virtual ТипыПользователей ТипПользователяNavigation { get; set; } = null!;
}
