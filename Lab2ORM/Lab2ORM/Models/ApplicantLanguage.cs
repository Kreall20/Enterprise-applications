using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class ApplicantLanguage
{
    public string Language { get; set; } = null!;

    public int Applicant { get; set; }

    public int Id { get; set; }

    public virtual Applicant ApplicantNavigation { get; set; } = null!;
}
