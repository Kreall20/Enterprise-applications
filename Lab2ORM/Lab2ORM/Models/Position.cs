using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Position
{
    public int Positionid { get; set; }

    public string Position1 { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; } = new List<Applicant>();

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
