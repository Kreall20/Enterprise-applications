using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class ApplicantToVacancy
{
    public int Id { get; set; }

    public int Employerid { get; set; }

    public int Vacancyid { get; set; }

    public string Userflag { get; set; } = null!;

    public int Userid { get; set; }

    public virtual Applicant User { get; set; } = null!;

    public virtual Vacancy Vacancy { get; set; } = null!;
}
