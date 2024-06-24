using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Education
{
    public int Educationid { get; set; }

    public string Education1 { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; } = new List<Applicant>();

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
