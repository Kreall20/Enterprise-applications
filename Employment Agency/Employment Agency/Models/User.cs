using System;
using System.Collections.Generic;

namespace Employment_Agency.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int TypeofUser { get; set; }

    public virtual Applicant? Applicant { get; set; }

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    public virtual TypesofUser TypeofUserNavigation { get; set; } = null!;
}
