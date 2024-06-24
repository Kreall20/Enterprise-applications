using System;
using System.Collections.Generic;

namespace Lab2ORM.Classes;

public partial class ПесниПользователя
{
    public int КодМузыки { get; set; }

    public int UserId { get; set; }

    public string ПутьПесни { get; set; } = null!;

    public int Id { get; set; }

    public int Жанр { get; set; }
    public int Альбом { get; set; }
    public int Исполнитель { get; set; }
}
