using System;
using System.Collections.Generic;

namespace Employment_Agency.Models;

public partial class Employer
{
    public int Userid { get; set; }

    public string Company { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Fio { get; set; } = null!;

    public int Empoyerid { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
}
