using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Vacancy
{
    public int Vacancyid { get; set; }

    public int Employer { get; set; }

    public string? Gender { get; set; }

    public int Education { get; set; }

    public string WorkExperience { get; set; } = null!;

    public bool? PcKnowledge { get; set; }

    public string? Languages { get; set; }

    public int Position { get; set; }

    public bool? DriverLicense { get; set; }

    public DateTime PlacementDate { get; set; }

    public decimal Salary { get; set; }

    public string Schedule { get; set; } = null!;

    public string? City { get; set; }

    public bool Openness { get; set; }

    public virtual ICollection<ApplicantToVacancy> ApplicantToVacancies { get; } = new List<ApplicantToVacancy>();

    public virtual ICollection<Deal> Deals { get; } = new List<Deal>();

    public virtual Education EducationNavigation { get; set; } = null!;

    public virtual Employer EmployerNavigation { get; set; } = null!;

    public virtual Position PositionNavigation { get; set; } = null!;

    public virtual ICollection<VacancyLanguage> VacancyLanguages { get; } = new List<VacancyLanguage>();
}
