using System;
using System.Collections.Generic;

namespace Employment_Agency.Models;

public partial class Education
{
    public int Educationid { get; set; }

    public string Education1 { get; set; } = null!;

    public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();

    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}
