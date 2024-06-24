using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Deal
{
    public int Dealid { get; set; }

    public int Vacancy { get; set; }

    public decimal Commission { get; set; }

    public DateTime Dateofpreparation { get; set; }

    public virtual ICollection<DealApplicant> DealApplicants { get; } = new List<DealApplicant>();

    public virtual Vacancy VacancyNavigation { get; set; } = null!;
}
