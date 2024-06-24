using System;
using System.Collections.Generic;

namespace Employment_Agency.Models;

public partial class TypesofUser
{
    public int Typeid { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
