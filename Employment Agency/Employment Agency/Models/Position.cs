using System;
using System.Collections.Generic;

namespace Employment_Agency.Models;

public partial class Position
{
    public int Positionid { get; set; }

    public string Position1 { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}
