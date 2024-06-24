using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class Employer
{
    public int Employerid { get; set; }

    public string Company { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Fio { get; set; } = null!;

    public virtual User EmployerNavigation { get; set; } = null!;

    public virtual ICollection<Vacancy> Vacancies { get; } = new List<Vacancy>();
}
