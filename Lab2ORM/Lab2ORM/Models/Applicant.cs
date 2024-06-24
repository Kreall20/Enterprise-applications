using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Applicant
{
    public int Applicantid { get; set; }

    public string Fio { get; set; } = null!;

    public DateTime PlacementDate { get; set; }

    public decimal? Salary { get; set; }

    public string Schedule { get; set; } = null!;

    public bool DriverLicense { get; set; }

    public string? Languages { get; set; }

    public DateTime? DateofBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int Position { get; set; }

    public int Education { get; set; }

    public string? City { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string WorkExperience { get; set; } = null!;

    public bool? PcKnowledge { get; set; }

    public virtual ICollection<ApplicantLanguage> ApplicantLanguages { get; } = new List<ApplicantLanguage>();

    public virtual User ApplicantNavigation { get; set; } = null!;

    public virtual ICollection<ApplicantToVacancy> ApplicantToVacancies { get; } = new List<ApplicantToVacancy>();

    public virtual ICollection<DealApplicant> DealApplicants { get; } = new List<DealApplicant>();

    public virtual Education EducationNavigation { get; set; } = null!;

    public virtual Position PositionNavigation { get; set; } = null!;
}
