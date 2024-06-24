using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class VacancyLanguage
{
    public int Id { get; set; }

    public string Language { get; set; } = null!;

    public int Vacancy { get; set; }

    public virtual Vacancy VacancyNavigation { get; set; } = null!;
}
