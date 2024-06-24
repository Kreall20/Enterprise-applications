using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class ПесниПользователя
{
    public int КодМузыки { get; set; }

    public int UserId { get; set; }

    public string ПутьПесни { get; set; } = null!;

    public int Id { get; set; }

    public string Жанр { get; set; } = null!;

    public string? Альбом { get; set; }

    public string Исполнитель { get; set; } = null!;

    public string? Плейлист { get; set; }

    public string UserName { get; set; } = null!;

    public virtual Пользователи User { get; set; } = null!;

    public virtual Песни КодМузыкиNavigation { get; set; } = null!;
}
