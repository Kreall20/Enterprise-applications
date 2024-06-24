using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class DealApplicant
{
    public int Id { get; set; }

    public int Dealid { get; set; }

    public int Applicantid { get; set; }

    public virtual Applicant Applicant { get; set; } = null!;

    public virtual Deal Deal { get; set; } = null!;
}
